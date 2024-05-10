import {RecipeIngredient} from "./recipeIngredient.model";

export interface Recipe {
  id: number;
  title: string;
  description: string;
  recipeStatusName: string;
  UserKnownAs: string;
  isExclusive: string;
  recipeImageUrl: string;
  numberOfViews: number;
  completionTime: number;
  recipeComplexityName: string;
  RecipeIngredients: RecipeIngredient[];
}
