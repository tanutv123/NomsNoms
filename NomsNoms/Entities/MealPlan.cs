namespace NomsNoms.Entities
{
    public class MealPlan
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public int MealPlanTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public MealPlanType MealPlanType { get; set; }
        public ICollection<UserMealPlan> UserMealPlans { get; set; }
    }
}
