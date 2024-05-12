namespace NomsNoms.DTOs
{
    public class RecipeLikeToShowDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string RecipeStatusName { get; set; }
        public string UserKnownAs { get; set; }
        public bool IsExclusive { get; set; }
        public string RecipeImageUrl { get; set; }
        public int NumberOfViews { get; set; }
    }
}
