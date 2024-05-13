import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {RecipeComponent} from "./components/recipe/recipe.component";
import {MealPlanComponent} from "./components/meal-plan/meal-plan.component";
import {MealPlanBoughtComponent} from "./components/meal-plan-bought/meal-plan-bought.component";
import {RecipeDetailComponent} from "./components/recipe/recipe-detail/recipe-detail.component";
import {RecipeStepListComponent} from "./components/recipe/recipe-step-list/recipe-step-list.component";
import {ProfileComponent} from "./components/user/profile/profile.component";
import {LoginComponent} from "./components/account/login/login.component";
import {RegisterComponent} from "./components/account/register/register.component";
import {TestErrorComponent} from "./components/error/test-error/test-error.component";
import {ServerErrorComponent} from "./components/error/server-error/server-error.component";
import {NotfoundErrorComponent} from "./components/error/notfound-error/notfound-error.component";
import {ListOfRecipeComponent} from "./components/recipe/list-of-recipe/list-of-recipe.component";
import {recipeDetailResolver} from "./_resolvers/recipe-detail.resolver";

const routes: Routes = [
  {path: '', component: RecipeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path:'',
  children: [
    {path: 'meal-plan', component: MealPlanComponent},
    {path: 'meal-plan-bought', component: MealPlanBoughtComponent},
    {path: 'steps/:id', component: RecipeStepListComponent},
    {
      path: 'recipe/:id',
      component: RecipeDetailComponent,
      resolve: {recipe: recipeDetailResolver}
    },
    {path: 'list', component: ListOfRecipeComponent},
    {path: 'profile', component: ProfileComponent},
  ]
  },
  { path: 'server-error', component: ServerErrorComponent },

  { path: '**', component: NotfoundErrorComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
