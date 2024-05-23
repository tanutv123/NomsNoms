import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {RecipeAdmin} from "../_model/Admin/recipeAdmin.model";
import {Recipe} from "../_model/recipe.model";
import {TasteProfile} from "../_model/tasteProfile.model";
import {Register} from "../_model/register.model";
import {MealPlanSubscription} from "../_model/Manager/mealPlanSubscription.model";
import {MealPlan} from "../_model/mealPlan.model";

@Injectable({
  providedIn: 'root'
})
export class RecipeAdminService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getRecipe(id: number) {
    return this.http.get<RecipeAdmin>(this.baseUrl + 'admin/recipe/' + id);
  }

  getRecipes() {
    return this.http.get<Recipe[]>(this.baseUrl + 'admin/recipes');
  }
  getPendingRecipes() {
    return this.http.get<Recipe[]>(this.baseUrl + 'admin/pending-recipes');
  }

  acceptRecipe(recipeId: number, tasteProfile: TasteProfile) {
    return this.http.post(this.baseUrl + 'admin/pending-recipes/set-status-profile?recipeId='+recipeId, tasteProfile);
  }

  deleteRecipe(recipe: Recipe) {
    return this.http.delete(this.baseUrl + 'admin/recipes/delete-recipe', {
      body: recipe
    });
  }

  createUser(register: Register) {
    return this.http.post(this.baseUrl + 'admin/users/create-user', register);
  }

  getMealPlans() {
    return this.http.get<MealPlan[]>(this.baseUrl + 'admin/meal-plans');
  }
  getMealPlan(id: number) {
    return this.http.get<MealPlan>(this.baseUrl + 'admin/meal-plans/' + id);
  }

  deleteMealPlan(mealPlan: MealPlan) {
    return this.http.delete(this.baseUrl + 'admin/meal-plan/delete-meal-plan', {body: mealPlan});
  }

  createMealPlan(mealPlan: MealPlan) {
    return this.http.post(this.baseUrl + 'admin/meal-plan/create-meal-plan', mealPlan);
  }
  updateMealPlan(mealPlan: MealPlan) {
    return this.http.put(this.baseUrl + 'admin/meal-plan/update-meal-plan', mealPlan);
  }
  enableMealPlan(mealPlan: MealPlan) {
    return this.http.put(this.baseUrl + 'admin/meal-plan/enable-meal-plan', mealPlan);
  }
}
