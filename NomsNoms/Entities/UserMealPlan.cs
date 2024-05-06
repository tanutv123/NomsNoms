namespace NomsNoms.Entities
{
    public class UserMealPlan
    {
        public int MealPlanId { get; set; }
        public int AppUserId { get; set; }
        public DateTime StartedDate { get; set; }
        public AppUser AppUser { get; set; }
        public MealPlan MeanPlan { get; set; }
    }
}
