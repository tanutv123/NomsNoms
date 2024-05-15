using NomsNoms.DTOs;
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

        Task Like(string email, int recipeid);
        Task<List<RecipeLikeToShowDTO>> GetRecipeLikeByUserEmail(string email);
        Task<List<RecipeDTO>> GetRecipeForUser(int id);
        Task<List<RecipeDTO>> GetUserRecipeForProfile(string email);

        Task<List<RecipeStepDTO>> GetRecipeStepAsync(int id);
        Task AddRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetAllRecipes();
        Task<List<RecipeDTO>> GetAllRecipeAdmin();
        Task UpdateRecipe(RecipeUpdateDTO recipe);
        Task DeleteRecipe(RecipeUpdateDTO recipeDto);
        Task<List<RecipeStatus>> GetAllRecipeStatus();
        Task<RecipeUpdateDTO> GetRecipeById(int id);
    }
}
