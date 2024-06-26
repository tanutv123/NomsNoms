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
        Task<List<Ingredient>> GetIngredientsAsync();
        Task<RecipeDTO> GetRecipeAsync(int id);
        Task<ViewRecipeAdminDTO> GetRecipeAdminAsync(int id);

        Task Like(string email, int recipeid);
        Task<bool> IsLike(string email, int recipeid);
        Task<List<RecipeLikeToShowDTO>> GetRecipeLikeByUserEmail(string email);
        Task<List<RecipeDTO>> GetRecipeForUser(int id);
        Task<List<RecipeDTO>> GetUserRecipeForProfile(string email);

        Task<List<RecipeStepDTO>> GetRecipeStepAsync(int id, int userId);
        Task AddRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetAllRecipes();
        Task<List<RecipeDTO>> GetAllRecipeAdmin();
        Task UpdateRecipe(RecipeUpdateDTO recipe);
        Task DeleteRecipe(RecipeUpdateDTO recipeDto);
        Task<List<RecipeStatus>> GetAllRecipeStatus();
        Task<RecipeUpdateDTO> GetRecipeById(int id);
        Task<List<RecipeDTO>> GetAllPendingRecipeAdmin();
        Task SetTatseProfileAndStatus(int recipeId, TasteProfile tp);
        Task HideRecipe(int recipeid);
        Task<bool> IsOwnerRecipe(int recipeid, string userEmail);
        Task DeletedRecipe(int recipeid);
        Task<List<Recipe>> RecommendRecipes(TasteProfile userTaste, List<Recipe> allRecipes);
        Task<List<IngredientDTO>> GetIngredientsAdmin();
        Task<IngredientDTO> GetIngredientAdmin(int id);
        Task UpdateIngredient(IngredientDTO ingredientDTO);
        Task AddIngredientAsync(IngredientDTO ingredientDTO);
        Task DeleteIngredient(int ingredientId);
        Task EnableIngredient(int ingredientId);

        /*Task<float> CalculateRecipeCalories(int recipeId, List<RecipeIngredientCalculateDTO> ingredients);*/
        Task<int> CalculateRecipeCalories(ICollection<RecipeIngredient> ingredients);

    }
}
