﻿using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Helpers;

namespace NomsNoms.Interfaces
{
    public interface IRecipeRepository
    {
        Task<PagedList<RecipeDTO>> GetRecipesAsync(UserParams userParams);
        Task<List<RecipeDTO>> GetTrendingRecipe();
        Task<List<Category>> GetCategoriesAsync();
        Task<RecipeDTO> GetRecipeAsync(int id);
        Task Like(string email, int recipeid);
        Task<List<RecipeLikeToShowDTO>> GetRecipeLikeByUserEmail(string email);
    }
}
