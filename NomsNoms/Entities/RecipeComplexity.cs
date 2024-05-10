namespace NomsNoms.Entities
{
    public class RecipeComplexity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
