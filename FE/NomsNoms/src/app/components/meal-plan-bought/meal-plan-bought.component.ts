import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../_model/recipe.model";
import {MealPlan} from "../../_model/mealPlan.model";
import {MealPlanService} from "../../_services/meal-plan.service";

@Component({
  selector: 'app-meal-plan-bought',
  templateUrl: './meal-plan-bought.component.html',
  styleUrls: ['./meal-plan-bought.component.scss']
})
export class MealPlanBoughtComponent implements OnInit{
  recipes: Recipe[] = [];
  calories: number = 0;

  constructor(private mealPlanService: MealPlanService) {
  }

  ngOnInit() {
    this.mealPlanService.getMealPlanRecommendations().subscribe({
      next: recipes => {
        this.recipes = recipes;
        console.log(this.recipes);
        this.calories = this.recipes.reduce((x, y) => x + y.calories, 0);
      }
    });
  }

}
