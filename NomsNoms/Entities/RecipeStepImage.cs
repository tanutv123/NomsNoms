namespace NomsNoms.Entities
{
    public class RecipeStepImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
        public int RecipeStepId { get; set; }
        public RecipeStep RecipeStep { get; set; }
    }
}
