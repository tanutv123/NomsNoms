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
    public class MealPlanController : ControllerBase
    {
        private readonly IMealPlanRepository _mealPlanRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MealPlanController(IMealPlanRepository repository, 
            IRecipeRepository recipeRepository, 
            IUserRepository userRepository,
            IMapper mapper)
        {
            _mealPlanRepository = repository;
            _recipeRepository = recipeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetMealPlanSubscriptions()
        {
            return Ok(await _mealPlanRepository.GetMealPlanSubscriptionsAsync());
        }

        [HttpGet("type")]
        public async Task<IActionResult> GetAllType()
        {
            return Ok(await _mealPlanRepository.GetAllType());
        }
        [HttpGet("is-registered")]
        [Authorize]
        public async Task<IActionResult> GetUserMealPlanStatus ()
        {
            return Ok(await _mealPlanRepository.IsRegisterMealPlan(User.GetEmail()));
        }

        [HttpGet("recommendations")]
        [Authorize]
        public async Task<IActionResult> GetRecommendedRecipes()
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

            // Get the top 3 recommended recipes for the user
            List<Recipe> recommendedRecipes = await _mealPlanRepository.RecommendRecipes(userTaste, allRecipes);
            var result = _mapper.Map<List<RecipeDTO>>(recommendedRecipes);
            return Ok(result);
        }
    }
}
