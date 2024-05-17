namespace NomsNoms.Entities
{
    public class MealPlanSubscription
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<UserMealPlanSubscriptions> UserMealPlanSubscriptions { get; set; }
    }
}
