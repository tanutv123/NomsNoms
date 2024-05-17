import { ResolveFn } from '@angular/router';
import {inject} from "@angular/core";
import {RecipeService} from "../_services/recipe.service";
import {RecipeAdmin} from "../_model/Admin/recipeAdmin.model";
import {RecipeAdminService} from "../_services/recipe-admin.service";

export const recipeAdminResolver: ResolveFn<RecipeAdmin> = (route, state) => {
  const recipeService = inject(RecipeAdminService);
  return recipeService.getRecipe(+route.paramMap.get('id')!);
};
