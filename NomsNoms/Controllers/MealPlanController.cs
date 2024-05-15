using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Data;
using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanRepository _mealPlanRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        public MealPlanController(IMealPlanRepository repository, IRecipeRepository recipeRepository, IUserRepository userRepository)
        {
            _mealPlanRepository = repository;
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetAllType()
        {
            return Ok(await _mealPlanRepository.GetAllType());
        }

        [HttpGet("recommendations/{email}")]
        public async Task<IActionResult> GetRecommendedRecipes(string email)
        {
            // Get the user's taste profile from the repository
            TasteProfile userTaste = await _userRepository.GetUserTasteProfile(email);
            if (userTaste == null)
            {
                return NotFound("User taste profile not found");
            }

            // Get all recipes from the repository
            List<Recipe> allRecipes = await _recipeRepository.GetAllRecipes();

            // Get the top 3 recommended recipes for the user
            List<Recipe> recommendedRecipes = await _mealPlanRepository.RecommendRecipes(userTaste, allRecipes);

            return Ok(recommendedRecipes);
        }
    }
}
