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
import {NewRecipeComponent} from "./components/user/new-recipe/new-recipe.component";
import {MyRecipeComponent} from "./components/user/my-recipe/my-recipe.component";
import {userDetailResolver} from "./_resolvers/user-detail.resolver";
import {PaymentTestsComponent} from "./components/payment-tests/payment-tests.component";
import {SuccessComponent} from "./components/payment/success/success.component";
import {FailComponent} from "./components/payment/fail/fail.component";
import {ProfileEditComponent} from "./components/user/profile-edit/profile-edit.component";
import {UserManagementComponent} from "./components/admin/user-management/user-management.component";
import {RecipeManagementComponent} from "./components/admin/recipe-management/recipe-management.component";
import {
  RecipeDetailAdminComponent
} from "./components/admin/recipe-management/recipe-detail-admin/recipe-detail-admin.component";
import {recipeAdminResolver} from "./_resolvers/recipe-admin.resolver";

const routes: Routes = [
  {path: '', component: RecipeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'payment-test', component: PaymentTestsComponent},
  {path: 'payment-success', component: SuccessComponent},
  {path: 'payment-fail', component: FailComponent},
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
    {path: 'new-recipe', component: NewRecipeComponent},
    {path: 'my-recipe', component: MyRecipeComponent},
    {path: 'list', component: ListOfRecipeComponent},
    {path: 'profile/:email', component: ProfileComponent, resolve: {user: userDetailResolver}},
    {path: 'profile-edit/:email', component: ProfileEditComponent, resolve: {user: userDetailResolver}},
  ]
  },
  {path:'',
    children: [
      {path: 'user-management', component: UserManagementComponent},
      {path: 'recipe-management', component: RecipeManagementComponent},
      {path: 'recipe-admin/:id', component: RecipeDetailAdminComponent,
      resolve: {recipe: recipeAdminResolver}
      },
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
