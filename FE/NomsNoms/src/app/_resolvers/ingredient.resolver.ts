import { ResolveFn } from '@angular/router';
import {inject} from "@angular/core";
import {RecipeAdminService} from "../_services/recipe-admin.service";
import {Ingredient} from "../_model/ingredient.model";

export const ingredientResolver: ResolveFn<Ingredient> = (route, state) => {
  const adminService = inject(RecipeAdminService);
  return adminService.getIngredient(+route.paramMap.get('id')!);
};
