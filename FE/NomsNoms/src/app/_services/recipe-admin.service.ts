import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {RecipeAdmin} from "../_model/Admin/recipeAdmin.model";
import {Recipe} from "../_model/recipe.model";
import {TasteProfile} from "../_model/tasteProfile.model";

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
}
