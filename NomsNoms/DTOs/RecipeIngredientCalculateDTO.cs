using NomsNoms.Entities;

namespace NomsNoms.DTOs
{
    public class RecipeIngredientCalculateDTO
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public float IngredientServing { get; set; }
    }
}
