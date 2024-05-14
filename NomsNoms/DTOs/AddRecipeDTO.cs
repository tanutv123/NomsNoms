namespace NomsNoms.DTOs
{
    public class AddRecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsExclusive { get; set; }
        public int NumberOfViews { get; set; }
        public int CompletionTime { get; set; }
        public int RecipeComplexityId { get; set; }
        public ICollection<AddRecipeIngredientDTO> RecipeIngredients { get; set; }
        public ICollection<AddRecipeStepDTO> RecipeSteps { get; set; }
        
    }
}
