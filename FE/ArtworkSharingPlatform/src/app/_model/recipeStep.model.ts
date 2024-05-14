import {RecipeStepImage} from "./recipeStepImage.model";

export interface RecipeStep {
  id: number;
  no: number;
  recipeId: number;
  description: string;
  recipeStepImages: RecipeStepImage[];
}
