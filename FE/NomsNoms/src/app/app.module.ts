import {CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { BannerComponent } from './components/banner/banner.component';
import { FooterComponent } from './components/footer/footer.component';
import { RecipeComponent } from './components/recipe/recipe.component';
import { FilterComponent } from './components/recipe/filter/filter.component';
import { MealPlanComponent } from './components/meal-plan/meal-plan.component';
import { MealPlanBoughtComponent } from './components/meal-plan-bought/meal-plan-bought.component';
import { RecommendComponent } from './components/recipe/recommend/recommend.component';
import {Home, LucideAngularModule, Menu, UserCheck, File, RefreshCcw, TrendingUp, Clock,Wheat, Heart, Eye, Star, Zap  } from "lucide-angular";
import { TrendingComponent } from './components/recipe/trending/trending.component';
import { RecipeDetailComponent } from './components/recipe/recipe-detail/recipe-detail.component';
import { RecipeStepListComponent } from './components/recipe/recipe-step-list/recipe-step-list.component';
// import function to register Swiper custom elements
import { register } from 'swiper/element/bundle';
import { ProfileComponent } from './components/user/profile/profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import { RecipeListComponent } from './components/user/recipe-list/recipe-list.component';
import {RecipeCardComponent} from "./components/recipe/recipe-card/recipe-card.component";
import { RecipeCardProfileComponent } from './components/user/recipe-list/recipe-card-profile/recipe-card-profile.component';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import {ToastrModule} from "ngx-toastr";
import {NgxSpinnerModule} from "ngx-spinner";
import { TestErrorComponent } from './components/error/test-error/test-error.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {ErrorInterceptor} from "./_interceptors/error.interceptor";
import {LoadingInterceptor} from "./_interceptors/loading.interceptor";
import { ServerErrorComponent } from './components/error/server-error/server-error.component';
import { NotfoundErrorComponent } from './components/error/notfound-error/notfound-error.component';
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import { ListOfRecipeComponent } from './components/recipe/list-of-recipe/list-of-recipe.component';
import { RecipeCategoryComponent } from './components/recipe/recipe-category/recipe-category.component';
import {PaginationModule} from "ngx-bootstrap/pagination";
import { SwiperDirective } from './_directives/swiper.directive';
import { StepsComponent } from './components/recipe/recipe-step-list/steps/steps.component';
import { NewRecipeComponent } from './components/user/new-recipe/new-recipe.component';
import {FileUploadModule} from "ng2-file-upload";
import { PhotoEditorComponent } from './components/photo-editor/photo-editor.component';
import {JwtInterceptor} from "./_interceptors/jwt.interceptor";
import { MyRecipeComponent } from './components/user/my-recipe/my-recipe.component';
import {DataTablesModule} from "angular-datatables";
import { HasRoleDirective } from './_directives/has-role.directive';
import { UserManagementComponent } from './components/admin/user-management/user-management.component';
import { PaymentTestsComponent } from './components/payment-tests/payment-tests.component';
import { SuccessComponent } from './components/payment/success/success.component';
import { FailComponent } from './components/payment/fail/fail.component';
import { ProfileEditComponent } from './components/user/profile-edit/profile-edit.component';
import { TruncatePipe } from './_pipes/truncate.pipe';
import { RecipeManagementComponent } from './components/admin/recipe-management/recipe-management.component';
import { RecipeDetailAdminComponent } from './components/admin/recipe-management/recipe-detail-admin/recipe-detail-admin.component';
import {TimeagoModule} from "ngx-timeago";
import { Top10UserComponent } from './components/recipe/top-10-user/top-10-user.component';
import { PendingsComponent } from './components/admin/recipe-management/pendings/pendings.component';
import { TasteTestComponent } from './components/taste-test/taste-test.component';
import {MatSliderModule} from "@angular/material/slider";
import { TasteProfileComponent } from './components/admin/recipe-management/modal/taste-profile/taste-profile.component';
import {ModalModule} from "ngx-bootstrap/modal";
import {MatDialogModule} from "@angular/material/dialog";
import { TransactionsComponent } from './components/manager/transactions/transactions.component';
import { MealPlansComponent } from './components/manager/meal-plans/meal-plans.component';
import { SalaryManagementComponent } from './components/manager/salary-management/salary-management.component';
import { CreateUserComponent } from './components/admin/user-management/create-user/create-user.component';
import { RecommendListComponent } from './components/recipe/recommend/recommend-list/recommend-list.component';
import {NgSelectModule} from "@ng-select/ng-select";
import {TestsComponent} from "./components/tests/tests.component";
import { MealplanManagementComponent } from './components/admin/mealplan-management/mealplan-management.component';
import { MealplanCreateComponent } from './components/admin/mealplan-management/mealplan-create/mealplan-create.component';
import { MealplanDetailComponent } from './components/admin/mealplan-management/mealplan-detail/mealplan-detail.component';
import { IngredientManagementComponent } from './components/admin/ingredient-management/ingredient-management.component';
import { IngredientCreateComponent } from './components/admin/ingredient-management/ingredient-create/ingredient-create.component';
import { IngredientDetailComponent } from './components/admin/ingredient-management/ingredient-detail/ingredient-detail.component';
import {MatTooltipModule} from "@angular/material/tooltip";
// register Swiper custom elements
register();
@NgModule({
  bootstrap: [AppComponent],
  declarations: [
    AppComponent,
    NavBarComponent,
    BannerComponent,
    FooterComponent,
    RecipeComponent,
    FilterComponent,
    MealPlanComponent,
    MealPlanBoughtComponent,
    RecommendComponent,
    TrendingComponent,
    RecipeDetailComponent,
    RecipeCardComponent,
    RecipeStepListComponent,
    ProfileComponent,
    RecipeListComponent,
    RecipeCardProfileComponent,
    TextInputComponent,
    LoginComponent,
    RegisterComponent,
    TestErrorComponent,
    ServerErrorComponent,
    NotfoundErrorComponent,
    ListOfRecipeComponent,
    RecipeCategoryComponent,
    SwiperDirective,
    StepsComponent,
    NewRecipeComponent,
    PhotoEditorComponent,
    MyRecipeComponent,
    HasRoleDirective,
    UserManagementComponent,
    PaymentTestsComponent,
    SuccessComponent,
    FailComponent,
    ProfileEditComponent,
    TruncatePipe,
    RecipeManagementComponent,
    RecipeDetailAdminComponent,
    Top10UserComponent,
    PendingsComponent,
    TasteTestComponent,
    TasteProfileComponent,
    TransactionsComponent,
    MealPlansComponent,
    SalaryManagementComponent,
    CreateUserComponent,
    RecommendListComponent,
    TestsComponent,
    MealplanManagementComponent,
    MealplanCreateComponent,
    MealplanDetailComponent,
    IngredientManagementComponent,
    IngredientCreateComponent,
    IngredientDetailComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
    AppRoutingModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    FileUploadModule,
    DataTablesModule,
    MatSliderModule,
    NgSelectModule,
    MatTooltipModule,
    TimeagoModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    NgxSpinnerModule.forRoot({ type: 'line-scale-party' }),
    LucideAngularModule.pick({File, Home, Menu, UserCheck, RefreshCcw, TrendingUp, Wheat, Heart, Eye, Star, Clock, Zap })
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
