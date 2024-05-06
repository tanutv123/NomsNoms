namespace NomsNoms.Entities
{
    public class RecipeLike
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
