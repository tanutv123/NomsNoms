﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.DTOs;
using NomsNoms.Extensions;
using NomsNoms.Helpers;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<RecipeDTO>>> GetRecipes([FromQuery] UserParams userParams)
        {
            if (string.IsNullOrEmpty(userParams.OrderByCompletionTime))
            {
                userParams.OrderByCompletionTime = "asc";
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
            return Ok(await _recipeRepository.GetTrendingRecipe());
        }
    }
}