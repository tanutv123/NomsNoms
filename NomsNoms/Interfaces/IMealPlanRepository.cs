using NomsNoms.Entities;

namespace NomsNoms.Interfaces
{
    public interface IMealPlanRepository
    {
        Task<List<MealPlanType>> GetAllType();
    }
}