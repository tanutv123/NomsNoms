using NomsNoms.DTOs;
using NomsNoms.Entities;

namespace NomsNoms.Interfaces
{
    public interface IMealPlanRepository
    {
        Task<List<MealPlanType>> GetAllType();
        Task<List<MealPlanSubscription>> GetMealPlanSubscriptionsAsync();
        Task<MealPlanSubscription> GetMealPlanSubscriptionAsync(int id);
        Task<List<Recipe>> RecommendRecipes(TasteProfile userTaste, List<Recipe> allRecipes);
        Task RegistMealPlan(int userId, int mealplanid);
        Task<bool> IsRegisterMealPlan(string email);
        Task AddPaymentIntent(long orderCode, int userId, int mealPlanId);
        Task<MealPlanPayment> GetPaymentIntent(long orderCode);
        Task CreateMealPlan(MealPlanAdminDTO mealPlan);
        Task UpdateMealPlan(MealPlanAdminDTO mealPlan);
        Task DeleteMealPlan(MealPlanAdminDTO mealPlan);
    }
}