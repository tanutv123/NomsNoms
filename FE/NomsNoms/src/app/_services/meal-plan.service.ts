import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {MealPlan} from "../_model/mealPlan.model";
import {Recipe} from "../_model/recipe.model";

@Injectable({
  providedIn: 'root'
})
export class MealPlanService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMealPlans() {
    return this.http.get<MealPlan[]>(this.baseUrl + 'mealplan');
  }
  getMealPlanRecommendations() {
    return this.http.get<Recipe[]>(this.baseUrl + 'mealplan/recommendations');
  }
}
