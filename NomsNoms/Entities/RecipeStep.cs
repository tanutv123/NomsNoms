namespace NomsNoms.Entities
{
    public class RecipeStep
    {
        public int Id { get; set; }
        public int No { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeImage> RecipeStepImages { get; set; }
    }
}
