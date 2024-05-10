using NomsNoms.DTOs;
using NomsNoms.Helpers;

namespace NomsNoms.Interfaces
{
    public interface IRecipeRepository
    {
        Task<PagedList<RecipeDTO>> GetRecipesAsync(UserParams userParams);
    }
}
