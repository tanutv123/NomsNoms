import {Image} from "../image.model";

export interface AddRecipeStep {
  no: number;
  description: string;
  recipeStepImages: Image[];
}
