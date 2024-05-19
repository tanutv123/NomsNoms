namespace NomsNoms.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RecipeComplexityId { get; set; }
        public int NumberOfViews { get; set; }
        public int CompletionTime { get; set; }
        /*public string Ingredients { get; set; }*/
        public int RecipeStatusId { get; set; }
        public int AppUserId { get; set; }
        public bool IsExclusive { get; set; }
        public int? RecipeImageId { get; set; }
        public int Calories { get; set; }
        public int? TasteProfileId { get; set; }
        public DateTime CreateDate { get; set; }
        public RecipeComplexity RecipeComplexity { get; set; }
        public ICollection<RecipeStep> RecipeSteps { get; set; }
        public ICollection<RecipeLike> RecipeLikes { get; set; }
        public ICollection<CollectionRecipe> CollectionRecipes { get; set; }
        public RecipeImage RecipeImage { get; set; }
        public ICollection<RecipeCategory> RecipeCategories{ get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public AppUser AppUser { get; set; }
        public RecipeStatus RecipeStatus{ get; set; }
        public TasteProfile TastProfile { get; set; }

    }
}
