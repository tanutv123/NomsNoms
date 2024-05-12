using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class RecipeStepDTO
    {
        public int Id { get; set; }
        public int No { get; set; }
        public int RecipeId { get; set; }
        public string Description { get; set; }
        public RecipeDTO Recipe { get; set; }
        public ICollection<RecipeStepImageDTO> RecipeStepImages { get; set; }
    }
}
