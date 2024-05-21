using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public ManagerController(ITransactionRepository transactionRepository,
            IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
        }
        [HttpGet("transactions")]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> GetAllTransaction()
        {
            return Ok(await _transactionRepository.GetAllTransactionAdmin());
        }
        [HttpGet("export/transaction-list")]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> ExportTransactionList()
        {
            var workbook = await _transactionRepository.ExportTransactionList();
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TransactionList.xlsx");
            }
        }
        [HttpGet("meal-plan/subscription")]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> GetAllUserMealPlan()
        {
            return Ok(await _userRepository.GetUserMealPlan());
        }
        [HttpGet("export/mealPlan-subscription-list")]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> ExportMealPlanSubscriptionList()
        {
            var workbook = await _userRepository.ExportUserMealPlanList();
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MealPlanSubscriptionList.xlsx");
            }
        }
        [HttpGet("users/cook-salary")]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> GetCookSalaries()
        {
            return Ok(await _userRepository.GetCooksSalaries());
        }
        [HttpGet("export/user-salary-list")]
        [Authorize(Policy = "RequireManagerRole")]
        public async Task<IActionResult> ExportUserSalaryList()
        {
            var workbook = await _userRepository.ExportSalaryList();
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SalaryList.xlsx");
            }
        }
    }
}
