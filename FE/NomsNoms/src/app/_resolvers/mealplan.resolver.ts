import { ResolveFn } from '@angular/router';
import {inject} from "@angular/core";
import {RecipeAdminService} from "../_services/recipe-admin.service";
import {MealPlan} from "../_model/mealPlan.model";

export const mealplanResolver: ResolveFn<MealPlan> = (route, state) => {
  const adminService = inject(RecipeAdminService);
  return adminService.getMealPlan(+route.paramMap.get('id')!);
};
