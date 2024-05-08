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
import {Home, LucideAngularModule, Menu, UserCheck, File, RefreshCcw, TrendingUp, Wheat, Heart, Eye, Star  } from "lucide-angular";
import { TrendingComponent } from './components/recipe/trending/trending.component';
import { RecipeDetailComponent } from './components/recipe/recipe-detail/recipe-detail.component';
import { RecipeCardComponent } from './components/recipe/recipe-card/recipe-card.component';
import { RecipeStepListComponent } from './components/recipe-step-list/recipe-step-list.component';
// import function to register Swiper custom elements
import { register } from 'swiper/element/bundle';
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
    RecipeStepListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LucideAngularModule.pick({File, Home, Menu, UserCheck, RefreshCcw, TrendingUp, Wheat, Heart, Eye, Star    })
  ],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
