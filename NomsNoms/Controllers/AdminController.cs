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
        private readonly IMealPlanRepository _mealPlanRepository;
        public AdminController(IUserRepository userRepository, IMapper mapper, IRecipeRepository recipeRepository, ITransactionRepository transactionRepository, IMealPlanRepository mealPlanRepository) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _transactionRepository = transactionRepository;
            _mealPlanRepository = mealPlanRepository;
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
                    KnownAs = userDTO.KnownAs,
                    UserName = userDTO.Email,
                    Introduction = userDTO.Introduction,
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    LastActive = DateTime.Now
                };
                await _userRepository.CreateUserAdmin(user);

                //var userRole = new AppUserRole
                //{
                //    UserId = user.Id,
                //    RoleId = userDTO.Role
                //};

                //user.UserRoles = new List<AppUserRole> { userRole };

                //user.UserPhoto = new UserPhoto
                //{
                //    AppUserId = user.Id,
                //    Url = "https://randomuser.me/api/portraits/men/80.jpg"
                //};

                //await _userRepository.UpdateUserAdmin(user);
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
        [HttpGet("meal-plans")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetMealPlans()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mealPlanRepository.GetMealPlansAdmin();
            return Ok(result);
        }
        [HttpGet("meal-plans/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetMealPlan(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _mealPlanRepository.GetMealPlanAdmin(id);
            return Ok(result);
        }
        [HttpPost("meal-plan/create-meal-plan")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateMealPlan([FromBody] MealPlanAdminDTO mealPlan)
        {
            mealPlan.CreatedDate = DateTime.UtcNow;
            mealPlan.Status = 1;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _mealPlanRepository.CreateMealPlan(mealPlan);
            return Ok();
        }

        [HttpPut("meal-plan/update-meal-plan")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateMealPlan([FromBody] MealPlanAdminDTO mealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _mealPlanRepository.UpdateMealPlan(mealPlan);
            return Ok();
        }
        [HttpPut("meal-plan/enable-meal-plan")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EnableMealPlan([FromBody] MealPlanAdminDTO mealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _mealPlanRepository.EnableMealPlan(mealPlan);
            return Ok();
        }

        [HttpDelete("meal-plan/delete-meal-plan")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteMealPlan([FromBody] MealPlanAdminDTO mealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _mealPlanRepository.DeleteMealPlan(mealPlan);
            return Ok();
        }
        [HttpGet("ingredients")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetIngredients()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _recipeRepository.GetIngredientsAdmin());
        }
        [HttpGet("ingredient-detail/{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _recipeRepository.GetIngredientAdmin(id));
        }
        [HttpPut("ingredient/update-ingredient")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDTO ingredientDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _recipeRepository.UpdateIngredient(ingredientDTO);
            return Ok();
        }
        [HttpPost("ingredient/create-ingredient")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDTO ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ingredient.Status = 1;
            await _recipeRepository.AddIngredientAsync(ingredient);
            return Ok();
        }
        [HttpDelete("ingredient/delete-ingredient/{ingredientId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _recipeRepository.DeleteIngredient(ingredientId);
            return Ok();
        }
        [HttpPut("ingredient/enable-ingredient/{ingredientId}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> EnableIngredient(int ingredientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _recipeRepository.EnableIngredient(ingredientId);
            return Ok();
        }
    }
}
