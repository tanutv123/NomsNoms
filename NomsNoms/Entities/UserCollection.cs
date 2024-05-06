namespace NomsNoms.Entities
{
    public class UserCollection
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<CollectionRecipe> CollectionRecipes { get; set; }
    }
}
