import {RecipeIngredient} from "../recipeIngredient.model";
import {RecipeStep} from "../recipeStep.model";

export interface RecipeAdmin {
  id: number;
  title: string;
  description: string;
  recipeStatusName: string;
  userKnownAs: string;
  isExclusive: string;
  recipeImageUrl: string;
  numberOfViews: number;
  completionTime: number;
  recipeComplexityName: string;
  recipeSteps: RecipeStep[];
  recipeIngredients: RecipeIngredient[];
}
