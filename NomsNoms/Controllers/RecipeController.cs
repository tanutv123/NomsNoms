using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Data;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Extensions;
using NomsNoms.Helpers;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper, IUserRepository userRepository)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<RecipeDTO>>> GetRecipes([FromQuery] UserParams userParams)
        {
            if (string.IsNullOrEmpty(userParams.OrderByCompletionTime))
            {
                userParams.OrderByCompletionTime = "asc";
            }
            string categoryString = Request.Query["categories"];
            if(!string.IsNullOrEmpty(categoryString))
            {
                try
                {
                    int[] categoryIds = categoryString.Split(',').Select(int.Parse).ToArray();
                    userParams.CategoryIds = categoryIds;
                }
                catch (Exception ex)
                {
                    return BadRequest("Invalid categories");
                }
            }
            var recipes = await _recipeRepository.GetRecipesAsync(userParams);
            Response.AddPaginationHeader(new PaginationHeader(recipes.CurrentPage,
                                                                recipes.PageSize,
                                                                recipes.TotalCount,
                                                                recipes.TotalPage));
            return Ok(recipes);

        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingRecipe()
        {
            var result = await _recipeRepository.GetTrendingRecipe();
            return Ok(result);
        }
        [HttpGet("profile-recipe/{email}")]
        public async Task<IActionResult> GetProfileRecipe(string email)
        {
            var result = await _recipeRepository.GetUserRecipeForProfile(email);
            return Ok(result);
        }
        [HttpGet("user-recipe")]
        [Authorize]
        public async Task<IActionResult> GetUserRecipe()
        {
            var result = await _recipeRepository.GetRecipeForUser(User.GetUserId());
            return Ok(result);
        }
        [HttpGet("ingredient")]
        [Authorize]
        public async Task<IActionResult> GetIngredients()
        {
            var result = await _recipeRepository.GetIngredientsAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
        {
            try
            {
                var result = await _recipeRepository.GetRecipeAsync(id);
                if (result == null) return BadRequest("Không tìm thấy công thức");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("recipe-steps/{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipeSteps(int id)
        {
            try
            {
                if(!User.Identity.IsAuthenticated)
                {
                    return BadRequest("Công thức chỉ dành cho hội viên");
                }
                var result = await _recipeRepository.GetRecipeStepAsync(id, User.GetUserId());
                if (result == null) return BadRequest("Không tìm thấy nội dung");
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("category")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var result = await _recipeRepository.GetCategoriesAsync();
            return Ok(result);
        }
        [HttpPost("like/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> RecipeLike(int recipeId)
        {
            string email = User.GetEmail();
            await _recipeRepository.Like(email, recipeId);
            return Ok(new { message = "User followed successfully." });
        }
        [HttpGet("isLiked/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> IsRecipeLike(int recipeId)
        {
            string email = User.GetEmail();
            var result = await _recipeRepository.IsLike(email, recipeId);
            return Ok(result);
        }
        [HttpGet("recipeLiked")]
        [Authorize]
        public async Task<ActionResult<List<RecipeLikeToShowDTO>>> GetRecipeHasLiked()
        {
            var email = User.GetEmail();
            var result = await _recipeRepository.GetRecipeLikeByUserEmail(email);
            return Ok(result);
        }
        
        [HttpPost("add-recipe")]
        public async Task<IActionResult> AddRecipe(AddRecipeDTO addRecipeDTO)
        {
            var recipe = _mapper.Map<Recipe>(addRecipeDTO);
            recipe.AppUserId = User.GetUserId();
            try
            {
                await _recipeRepository.AddRecipeAsync(recipe);
            }
            catch (Exception ex)
            {
                return BadRequest("Lỗi khi tạo công thức");
            }
            return Ok();
            }

        [HttpPut("hideRecipe/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> HideRecipe(int recipeId)
        {
            var email = User.GetEmail();
            var isOwner = await _recipeRepository.IsOwnerRecipe(recipeId, email);
            if (isOwner)
            {
                await _recipeRepository.HideRecipe(recipeId);
                return Ok(new {message = "Others people now can't see your recipe"});
            } else
            {
                return BadRequest("Bạn không có quyền truy cập vào nội dung này !!!");
            }
        }
        [HttpPut("deleteRecipe/{recipeId}")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(int recipeId)
        {
            var email = User.GetEmail();
            var isOwner = await _recipeRepository.IsOwnerRecipe(recipeId, email);
            if (isOwner)
            {
                await _recipeRepository.DeletedRecipe(recipeId);
                return Ok(new { message = "Others people now can't see your recipe" });
            }
            else
            {
                return BadRequest("Bạn không có quyền truy cập vào nội dung này !!!");
            }
        }

        [HttpGet("recommendRecipe")]
        [Authorize]
        public async Task<IActionResult> RecommendRecipe(int recipeId)
        {
            // Get the user's taste profile from the repository
            var email = User.GetEmail();
            TasteProfile userTaste = await _userRepository.GetUserTasteProfile(email);
            if (userTaste == null)
            {
                return NotFound("User taste profile not found");
            }

            // Get all recipes from the repository
            List<Recipe> allRecipes = await _recipeRepository.GetAllRecipes();

            // Get the top recommended recipes for the user
            List<Recipe> recommendedRecipes = await _recipeRepository.RecommendRecipes(userTaste, allRecipes);
            var result = _mapper.Map<List<RecipeDTO>>(recommendedRecipes);
            return Ok(result);
        }
    }
}
