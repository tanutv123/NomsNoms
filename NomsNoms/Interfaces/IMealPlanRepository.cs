using NomsNoms.Entities;

namespace NomsNoms.Interfaces
{
    public interface IMealPlanRepository
    {
        Task<List<MealPlanType>> GetAllType();
        Task<List<Recipe>> RecommendRecipes(TasteProfile userTaste, List<Recipe> allRecipes);
    }
}