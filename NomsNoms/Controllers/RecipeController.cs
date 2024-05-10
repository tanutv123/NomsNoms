﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.DTOs;
using NomsNoms.Entities;
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
            return Ok(await _recipeRepository.GetTrendingRecipe());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDTO>> GetRecipe(int id)
        {
            var result = await _recipeRepository.GetRecipeAsync(id);
            if(result == null) return BadRequest("Không tìm thấy công thức");
            return Ok(result);
        }
        [HttpGet("category")]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var result = await _recipeRepository.GetCategoriesAsync();
            return Ok(result);
        }
    }
}
