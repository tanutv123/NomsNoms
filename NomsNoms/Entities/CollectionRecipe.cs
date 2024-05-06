namespace NomsNoms.Entities
{
    public class CollectionRecipe
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public int UserCollectionId { get; set; }
        public UserCollection UserCollection { get; set; }
    }
}
