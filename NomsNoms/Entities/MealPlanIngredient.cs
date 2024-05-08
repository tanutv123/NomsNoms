namespace NomsNoms.Entities
{
    public class MealPlanIngredient
    {
        public int MealPlanId { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public MealPlan MeanPlan { get; set; }
    }
}
