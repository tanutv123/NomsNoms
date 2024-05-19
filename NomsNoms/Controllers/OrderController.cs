using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net.payOS;
using Net.payOS.Types;
using NomsNoms.DTOs;
using NomsNoms.Extensions;
using NomsNoms.Interfaces;
using NomsNoms.Types;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PayOS _payOS;
        private readonly ILogger<OrderController> _logger;
        private readonly IMealPlanRepository _mealPlanRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(
            PayOS payOS, 
            ILogger<OrderController> logger,
            IMealPlanRepository mealPlanRepository,
            IUserRepository userRepository
            )
        {
            _payOS = payOS;
            _logger = logger;
            _mealPlanRepository = mealPlanRepository;
            _userRepository = userRepository;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentLink(CreatePaymentLinkRequest body)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                var mp = await _mealPlanRepository.GetMealPlanSubscriptionAsync(body.mealPlanId);
                ItemData item = new ItemData(body.productName, 1, Decimal.ToInt32(mp.Price));
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData(orderCode, body.price, body.description, items, body.cancelUrl, body.returnUrl);

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
                await _mealPlanRepository.AddPaymentIntent(orderCode, User.GetUserId(), mp.Id);
                return Ok(new Response(0, "success", createPayment));
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }
        }

        [HttpPost("create-subscription")]
        [Authorize]
        public async Task<IActionResult> CreateSubscriptionPaymentLink(CreateSubscriptionPaymentLinkRequest body)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                var subscription = await _userRepository.GetSubscriptionById(body.subscriptionId);
                ItemData item = new ItemData(body.productName, 1, Decimal.ToInt32(subscription.Price));
                List<ItemData> items = new List<ItemData>();
                items.Add(item);
                PaymentData paymentData = new PaymentData(orderCode, body.price, body.description, items, body.cancelUrl, body.returnUrl);

                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
                await _userRepository.AddPaymentIntent(orderCode, User.GetUserId(), body.subscriptionId);
                return Ok(new Response(0, "success", createPayment));
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            try
            {
                PaymentLinkInformation paymentLinkInformation = await _payOS.getPaymentLinkInformation(orderId);
                var status = paymentLinkInformation.status == "PAID";
                if (status)
                {
                    var intent = await _mealPlanRepository.GetPaymentIntent(paymentLinkInformation.orderCode);
                    if (intent != null)
                    {
                        await _mealPlanRepository.RegistMealPlan(intent.AppUserId, intent.MealPlanId);
                        var transaction = new TransactionDTO
                        {
                            SenderId = intent.AppUserId,
                            ReportName = $"User {intent.AppUserId} đã thực hiện giao dịch mua meal plan {intent.MealPlanId}",
                            CreateDate = DateTime.UtcNow,
                            TotalPrice = paymentLinkInformation.amountPaid
                        };
                        await _userRepository.AddTransaction(transaction);
                    }
                    else
                    {
                        var subscriptionIntent = await _userRepository.GetPaymentIntent(paymentLinkInformation.orderCode);
                        if(subscriptionIntent != null)
                        {
                            await _userRepository.BuySubscription(subscriptionIntent.AppUserId, subscriptionIntent.SubscriptionId);
                            var transaction = new TransactionDTO
                            {
                                SenderId = intent.AppUserId,
                                ReportName = $"User {subscriptionIntent.AppUserId} đã thực hiện giao dịch mua gói hội viên {subscriptionIntent.SubscriptionId}",
                                CreateDate = DateTime.UtcNow,
                                TotalPrice = paymentLinkInformation.amountPaid
                            };
                            await _userRepository.AddTransaction(transaction);
                        }
                    }
                }
                return Ok(new Response(0, "Ok", paymentLinkInformation));
            }
            catch (System.Exception exception)
            {

                Console.WriteLine(exception);
                return Ok(new Response(-1, "fail", null));
            }

        }
    }
}
