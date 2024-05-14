namespace NomsNoms.DTOs
{
    public class AddRecipeDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsExclusive { get; set; }
        public int NumberOfViews { get; set; }
        public int CompletionTime { get; set; }
        public int RecipeComplexityId { get; set; }
        public AddRecipeImageDTO RecipeImage { get; set; }
        public ICollection<AddRecipeIngredientDTO> RecipeIngredients { get; set; }
        public ICollection<AddRecipeCategoryDTO> RecipeCategories { get; set; }
        public ICollection<AddRecipeStepDTO> RecipeSteps { get; set; }
        
    }
}
