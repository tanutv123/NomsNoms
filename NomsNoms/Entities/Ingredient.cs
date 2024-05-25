namespace NomsNoms.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public string Description { get; set; }
        public float Calories { get; set; }
        public byte Status { get; set; }
        public ICollection<MealPlanIngredient> MealPlanIngredients { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredient { get; set; }
    }
}
