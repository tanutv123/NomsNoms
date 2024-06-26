import {RecipeIngredient} from "./recipeIngredient.model";

export interface Recipe {
  id: number;
  title: string;
  description: string;
  recipeStatusName: string;
  userKnownAs: string;
  userEmail: string;
  isExclusive: string;
  recipeImageUrl: string;
  numberOfViews: number;
  numberOfLikes: number;
  calories: number;
  completionTime: number;
  recipeComplexityName: string;
  recipeIngredients: RecipeIngredient[];
}
