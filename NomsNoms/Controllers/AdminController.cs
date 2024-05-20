using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;
        private readonly ITransactionRepository _transactionRepository;
        public AdminController(IUserRepository userRepository, IMapper mapper, IRecipeRepository recipeRepository, ITransactionRepository transactionRepository) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpGet("users")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userRepository.GetAllUserAdmin());
        }
        [HttpGet("recipe/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            return Ok(await _recipeRepository.GetRecipeAdminAsync(id));
        }
        [HttpPost("users/create-user")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateAdminDTO userDTO)
        {
            try
            {
                var user = new AppUser
                {
                    Email = userDTO.Email,
                    PhoneNumber = userDTO.PhoneNumber,
                    KnownAs = userDTO.Name,
                    UserName = userDTO.Email,
                    Introduction = userDTO.Introduction,
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    LastActive = DateTime.Now
                };
                await _userRepository.CreateUserAdmin(user);

                var userRole = new AppUserRole
                {
                    UserId = user.Id,
                    RoleId = userDTO.Role
                };

                user.UserRoles = new List<AppUserRole> { userRole };

                user.UserPhoto = new UserPhoto
                {
                    AppUserId = user.Id,
                    Url = "https://randomuser.me/api/portraits/men/80.jpg"
                };

                await _userRepository.UpdateUserAdmin(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("users/update-user")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateUserAdmin([FromBody] UserAdminDTO userDto)
        {
            try
            {
                var user = _mapper.Map<AppUser>(userDto);
                await _userRepository.UpdateUserAdmin(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated Successfully");
        }

        [HttpDelete("users/delete-user")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteUserAdmin([FromBody] UserAdminDTO userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);
            await _userRepository.DeleteUserAdmin(user);
            return Ok();
        }

        [HttpGet("recipes")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllRecipeAdmin()
        {
            return Ok(await _recipeRepository.GetAllRecipeAdmin());
        }
        [HttpGet("recipes/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            return Ok(await _recipeRepository.GetRecipeById(id));
        }

        [HttpPut("recipes/update-recipe")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateRecipe([FromBody] RecipeUpdateDTO recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _recipeRepository.UpdateRecipe(recipe);
            return Ok("Updated successfully");
        }

        [HttpDelete("recipes/delete-recipe")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteRecipe([FromBody] RecipeUpdateDTO recipe)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _recipeRepository.DeleteRecipe(recipe);
            return Ok();
        }

        [HttpPut("users/enable-user/{email}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EnableUserAdmin(string email)
        {
            await _userRepository.EnableUserAdmin(email);
            return Ok();
        }

        [HttpGet("pending-recipes")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllPendingRecipes()
        {
            return Ok(await _recipeRepository.GetAllPendingRecipeAdmin());
        }

        [HttpPost("pending-recipes/set-status-profile")]
        public async Task<IActionResult> SetTasteProfileAndStatus([FromQuery] int recipeId, [FromBody] TasteProfile tp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _recipeRepository.SetTatseProfileAndStatus(recipeId, tp);
            return Ok();
        }
        [HttpGet("transactions")]
        public async Task<IActionResult> GetAllTransaction()
        {
            return Ok(await _transactionRepository.GetAllTransactionAdmin());
        }
        [HttpGet("meal-plan/subscription")]
        public async Task<IActionResult> GetAllUserMealPlan()
        {
            return Ok(await _userRepository.GetUserMealPlan());
        }
        [HttpGet("users/cook-salary")]
        public async Task<IActionResult> GetCookSalaries()
        {
            return Ok(await _userRepository.GetCooksSalaries());
        }
    }
}
