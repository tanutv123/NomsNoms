using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Data;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Extensions;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        [HttpGet("profile/{email}")]
        public async Task<IActionResult> GetUserProfile(string email)
        {
            var user = await _userRepository.GetUserProfile(email);
            return Ok(user);
        }
        [HttpPut("edit-profile")]
        /*[Authorize]*/
        public async Task<IActionResult> EditUserProfile([FromBody] UserProfileDTO profileDTO)
        {
            try
            {
                profileDTO.Email = User.GetEmail();
                var user = _mapper.Map<AppUser>(profileDTO);
                await _userRepository.UpdateUserDetail(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPost("follow/{email}")]
        [Authorize]
        public async Task<IActionResult> UserFollow(string email)
        {
            if (email == User.GetEmail()) return BadRequest("You cannot follow yourself");
            await _userRepository.Follow(email, User.GetUserId());
            return Ok(new { message = "User followed successfully." });
        }
        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10Cook()
        {
            return Ok(await _userRepository.GetTopCookByFollower());
        }
        [HttpGet("followers/{email}")]
        public async Task<IActionResult> GetFollowers(string email)
        {
            return Ok(await _userRepository.GetFollowerByCookId(email));
        }
        [HttpPost("subsctiption/buy/{subcriptionTypeId}")]
        [Authorize]
        public async Task<IActionResult> BuySubscription(string cookEmail, int subcriptionTypeId)
        {
            if (cookEmail == User.GetEmail()) return BadRequest("You cannot buy your own subscription");
            await _userRepository.BuySubscription(cookEmail ,User.GetEmail(), subcriptionTypeId);
            return Ok(new { message = "Buying Subscription successfully." });
        }
        [HttpGet("subsctiption/{cookEmail}")]
        [Authorize]
        public async Task<IActionResult> HasSubed(string cookEmail)
        {
            return Ok(await _userRepository.HasUserHasAlreadySub(cookEmail, User.GetEmail()));
        }
        [HttpGet("view/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> ViewRecipe(int recipeId)
        {
            await _userRepository.RecipeView(recipeId);
            return Ok();
        }

        [HttpPut("profile/userphoto")]
        [Authorize]
        public async Task<IActionResult> UpdateAvatar([FromBody] UserPhotoDTO userPhotoDTO)
        {
            userPhotoDTO.AppUserId = User.GetUserId();
            await _userRepository.UpdateUserPhoto(userPhotoDTO);
            return Ok(new { message = "Update Avatar successfully." });
        }

        [HttpGet("user-transaction")]
        [Authorize]
        public async Task<ActionResult<List<Transaction>>> GetUserTranstion()
        {
            var email = User.GetEmail();
            var transaction = await _userRepository.GetUserTransaction(email);
            return Ok(transaction);
        }
        [HttpGet("transaction")]
        [Authorize]
        public async Task<ActionResult<List<Transaction>>> GetAllUserTranstion()
        {            
            var transaction = await _userRepository.GetAllUserTransaction();
            return Ok(transaction);
        }

        [HttpPost("transaction")]
        [Authorize]
        public async Task<IActionResult> AddTransaction([FromBody] TransactionDTO transaction)
        {
            var email = User.GetEmail();
            transaction.SenderId = User.GetUserId();
            transaction.CreateDate = DateTime.Now;
            await _userRepository.AddTransaction(transaction);
            return Ok(new { message = "Thanh toán thành công." });
        }
        [HttpGet("subscription-list-sub/{email}")]
        public async Task<IActionResult> GetSubbers(string email)
        {
            return Ok(await _userRepository.GetSubberByCookId(email));
        }

        [HttpPost("userfollow/{cookEmail}")]
        [Authorize]
        public async Task<IActionResult> RegistMealPlan(string cookEmail)
        {
            var email = User.GetEmail();
            await _userRepository.FollowUser(cookEmail, email);
            return Ok();
        }

        [HttpPut("usertasteprofile/set")]
        [Authorize]
        public async Task<IActionResult> SetUserTasteProfile([FromBody] TasteProfileDTO tasteProfileDTO)
        {
            var email = User.GetEmail();
            await _userRepository.SetTasteProfileUser(tasteProfileDTO, email);
            return Ok();
        }
    }
}
