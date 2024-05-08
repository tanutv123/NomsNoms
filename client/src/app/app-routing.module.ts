import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {RecipeComponent} from "./components/recipe/recipe.component";
import {MealPlanComponent} from "./components/meal-plan/meal-plan.component";
import {MealPlanBoughtComponent} from "./components/meal-plan-bought/meal-plan-bought.component";
import {RecipeDetailComponent} from "./components/recipe/recipe-detail/recipe-detail.component";
import {RecipeStepListComponent} from "./components/recipe-step-list/recipe-step-list.component";

const routes: Routes = [
  {path: '', component: RecipeComponent},
  {path:'',
  children: [
    {path: 'meal-plan', component: MealPlanComponent},
    {path: 'meal-plan-bought', component: MealPlanBoughtComponent},
    {path: 'recipe-detail', component: RecipeDetailComponent},
    {path: 'recipe-step-list', component: RecipeStepListComponent},
  ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
