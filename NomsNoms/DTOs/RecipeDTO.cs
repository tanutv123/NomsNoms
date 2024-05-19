using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RecipeStatusId { get; set; }  
        public string RecipeStatusName { get; set; }
        public string UserKnownAs { get; set; }
        public bool IsExclusive { get; set; }
        public string RecipeImageUrl { get; set; }
        public int NumberOfViews { get; set; }
        public int Calories { get; set; }
        public int CompletionTime { get; set; }
        public int NumberOfLikes { get; set; }
        public string RecipeComplexityName { get; set; }
        public ICollection<RecipeIngredientDTO> RecipeIngredients { get; set; }
    }
}
