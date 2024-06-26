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
import {PendingsComponent} from "./components/admin/recipe-management/pendings/pendings.component";
import {authGuard} from "./_guards/auth.guard";
import {adminGuard} from "./_guards/admin.guard";
import {TasteTestComponent} from "./components/taste-test/taste-test.component";
import {TransactionsComponent} from "./components/manager/transactions/transactions.component";
import {managerGuard} from "./_guards/manager.guard";
import {MealPlansComponent} from "./components/manager/meal-plans/meal-plans.component";
import {SalaryManagementComponent} from "./components/manager/salary-management/salary-management.component";
import {CreateUserComponent} from "./components/admin/user-management/create-user/create-user.component";
import {RecommendListComponent} from "./components/recipe/recommend/recommend-list/recommend-list.component";
import {TestsComponent} from "./components/tests/tests.component";
import {MealplanManagementComponent} from "./components/admin/mealplan-management/mealplan-management.component";
import {
  MealplanCreateComponent
} from "./components/admin/mealplan-management/mealplan-create/mealplan-create.component";
import {
  MealplanDetailComponent
} from "./components/admin/mealplan-management/mealplan-detail/mealplan-detail.component";
import {mealplanResolver} from "./_resolvers/mealplan.resolver";
import {IngredientManagementComponent} from "./components/admin/ingredient-management/ingredient-management.component";
import {
  IngredientCreateComponent
} from "./components/admin/ingredient-management/ingredient-create/ingredient-create.component";
import {
  IngredientDetailComponent
} from "./components/admin/ingredient-management/ingredient-detail/ingredient-detail.component";
import {ingredientResolver} from "./_resolvers/ingredient.resolver";

const routes: Routes = [
  {path: '', component: RecipeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'payment-test', component: PaymentTestsComponent},
  {path: 'payment-success', component: SuccessComponent},
  {path: 'payment-fail', component: FailComponent},
  {path: 'testers', component: TestsComponent},
  {path:'',
  children: [
    {path: 'meal-plan', component: MealPlanComponent},
    {path: 'steps/:id', component: RecipeStepListComponent},
    {
      path: 'recipe/:id',
      component: RecipeDetailComponent,
      resolve: {recipe: recipeDetailResolver}
    },
    {path: 'taste-test', component: TasteTestComponent},
    {path: 'list', component: ListOfRecipeComponent},
    {path: 'profile/:email', component: ProfileComponent, resolve: {user: userDetailResolver}},
  ]
  },
  {path:'',
    runGuardsAndResolvers:'always',
    canActivate: [authGuard],
    children: [
      {path: 'user-management', component: UserManagementComponent, canActivate: [adminGuard]},
      {path: 'recipe-management', component: RecipeManagementComponent, canActivate: [adminGuard]},
      {path: 'new-recipe', component: NewRecipeComponent},
      {path: 'profile-edit/:email', component: ProfileEditComponent, resolve: {user: userDetailResolver}},
      {path: 'my-recipe', component: MyRecipeComponent},
      {path: 'recommend-list', component: RecommendListComponent},
      {path: 'meal-plan-bought', component: MealPlanBoughtComponent},
      {path: 'transactions', component: TransactionsComponent, canActivate: [managerGuard]},
      {path: 'meal-plans', component: MealPlansComponent, canActivate: [managerGuard]},
      {path: 'salary', component: SalaryManagementComponent, canActivate: [managerGuard]},
      {path: 'create-user', component: CreateUserComponent, canActivate: [adminGuard]},
      {path: 'pendings', component: PendingsComponent, canActivate: [adminGuard]},
      {path: 'ingredients', component: IngredientManagementComponent, canActivate: [adminGuard]},
      {path: 'ingredients/:id',
        component: IngredientDetailComponent,
        canActivate: [adminGuard],
        resolve: {ingredient: ingredientResolver}
      },
      {path: 'create-ingredient', component: IngredientCreateComponent, canActivate: [adminGuard]},
      {path: 'admin-mp', component: MealplanManagementComponent, canActivate: [adminGuard]},
      {path: 'admin-mp/:id',
        component: MealplanDetailComponent,
        resolve: {mealPlan: mealplanResolver},
        canActivate: [adminGuard]
      },
      {path: 'admin-create-mp', component: MealplanCreateComponent, canActivate: [adminGuard]},
      {path: 'recipe-admin/:id', component: RecipeDetailAdminComponent,
        resolve: {recipe: recipeAdminResolver},
        canActivate: [adminGuard]
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
