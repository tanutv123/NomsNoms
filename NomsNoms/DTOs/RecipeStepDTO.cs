using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class RecipeStepDTO
    {
        public int Id { get; set; }
        public int No { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeImage> RecipeStepImages { get; set; }
    }
}
