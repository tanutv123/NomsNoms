namespace NomsNoms.Entities
{
    public class UserMealPlanSubscriptions
    {
        public int AppUserId { get; set; }
        public int MealPlanSubscriptionId { get; set; }
        public DateTime StartedDate { get; set; }
        public AppUser AppUser { get; set; }
        public MealPlanSubscription MealPlanSubscription { get; set; }
    }
}
