using System.ComponentModel.DataAnnotations;

namespace NomsNoms.Entities
{
    public class MealPlanPayment
    {
        [Key]
        public long OrderCode{ get; set; }
        public int AppUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public AppUser AppUser { get; set; }
        public int MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; }
    }
}
