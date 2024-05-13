using Microsoft.EntityFrameworkCore;
using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Data
{
    public class MealPlanRepository : IMealPlanRepository
    {
        private readonly DataContext _context;
        public MealPlanRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<MealPlanType>> GetAllType()
        {
            List<MealPlanType> list = null;
            try
            {
                list = await _context.MealPlanTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
