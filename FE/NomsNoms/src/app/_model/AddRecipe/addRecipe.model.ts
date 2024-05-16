import {AddRecipeIngredient} from "./AddRecipeIngredient.model";
import {AddRecipeCategory} from "./addRecipeCategory.model";
import {Image} from "../image.model";
import {AddRecipeStep} from "./addRecipeStep.model";

export interface AddRecipe {
  title: string;
  description: string;
  isExclusive: boolean;
  numberOfViews: number;
  completionTime: number;
  recipeComplexityId: number;
  recipeImage: Image;
  recipeIngredients: AddRecipeIngredient[];
  recipeCategories: AddRecipeCategory[];
  recipeSteps: AddRecipeStep[];
}
