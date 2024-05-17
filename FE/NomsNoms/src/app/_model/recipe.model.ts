import {RecipeIngredient} from "./recipeIngredient.model";

export interface Recipe {
  id: number;
  title: string;
  description: string;
  recipeStatusName: string;
  userKnownAs: string;
  isExclusive: string;
  recipeImageUrl: string;
  numberOfViews: number;
  numberOfLikes: number;
  completionTime: number;
  recipeComplexityName: string;
  recipeIngredients: RecipeIngredient[];
}
