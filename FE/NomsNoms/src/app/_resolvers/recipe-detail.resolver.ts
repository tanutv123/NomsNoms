import { ResolveFn } from '@angular/router';
import {inject} from "@angular/core";
import {RecipeService} from "../_services/recipe.service";

export const recipeDetailResolver: ResolveFn<boolean> = (route, state) => {
  const recipeService = inject(RecipeService);
  return recipeService.getRecipe(+route.paramMap.get('id')!);
};
