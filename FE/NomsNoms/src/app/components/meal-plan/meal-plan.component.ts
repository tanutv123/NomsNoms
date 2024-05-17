import {Component, OnInit} from '@angular/core';
import {MealPlan} from "../../_model/mealPlan.model";
import {MealPlanService} from "../../_services/meal-plan.service";

@Component({
  selector: 'app-meal-plan',
  templateUrl: './meal-plan.component.html',
  styleUrls: ['./meal-plan.component.scss']
})
export class MealPlanComponent implements OnInit{
  mealPlans: MealPlan[] = [];
  constructor(private mpService: MealPlanService) {
  }

  ngOnInit() {
    this.loadMealPlans();
  }

  loadMealPlans() {
    this.mpService.getMealPlans().subscribe({
      next: mp => {
        this.mealPlans = mp;
      }
    })
  }
}
