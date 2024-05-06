namespace NomsNoms.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public int RecipeStatusId { get; set; }
        public bool IsExclusive { get; set; }
        public int? RecipeImageId { get; set; }
        public ICollection<RecipeStep> RecipeSteps { get; set; }
        public ICollection<RecipeLike> RecipeLikes { get; set; }
        public ICollection<CollectionRecipe> CollectionRecipes { get; set; }
        public RecipeImage RecipeImage { get; set; }
        public ICollection<RecipeCategory> RecipeCategories{ get; set; }
        public RecipeStatus RecipeStatus{ get; set; }

    }
}
