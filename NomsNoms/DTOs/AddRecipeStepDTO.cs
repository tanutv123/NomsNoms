﻿namespace NomsNoms.DTOs
{
    public class AddRecipeStepDTO
    {
        public int No { get; set; }
        public string Description { get; set; }
        public ICollection<RecipeStepImageDTO> RecipeStepImages { get; set; }
    }
}
