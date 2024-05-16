import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Category} from "../_model/category.model";
import {UserParams} from "../_model/userParams.model";
import {Recipe} from "../_model/recipe.model";
import {map, of} from "rxjs";
import {getPaginatedResult, getPaginationHeaders} from "./pagination-helper.service";
import {RecipeStep} from "../_model/recipeStep.model";
import {AddRecipe} from "../_model/AddRecipe/addRecipe.model";
import {Ingredient} from "../_model/ingredient.model";

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  baseUrl = environment.apiUrl;
  userParams: UserParams = {
    orderByCompletionTime: 'asc',
    orderByComplexity: 'asc',
    pageNumber: 1,
    pageSize: 6,
    search: '',
    categoryIds: [1]
  };
  defaultUserParams: UserParams = {
    orderByCompletionTime: 'asc',
    orderByComplexity: 'asc',
    pageNumber: 1,
    pageSize: 6,
    search: '',
    categoryIds: [1]
  };
  recipeCache = new Map();
  recipe: Recipe[] = [];
  constructor(private http: HttpClient) { }
  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = this.defaultUserParams;
  }

  getRecipes(userParam: UserParams) {
    //Getting the query values from the cache
    const response = this.recipeCache.get(Object.values(userParam).join('-'));
    //Return an observable with response value if it is contained in the cache
    //If it is not contained in the cache, it will send a request to the server
    if (response) return of(response);
    let params = getPaginationHeaders(userParam.pageNumber, userParam.pageSize);
    params = params.append('orderByCompletionTime', userParam.orderByCompletionTime);
    params = params.append('search', userParam.search);
    if (userParam.categoryIds.length > 0) {
      params = params.append('categories', userParam.categoryIds.join(','));
    }
    //Use map() method in this GET method in order to set a key-value pairs for our cache value
    return getPaginatedResult<Recipe[]>(this.baseUrl + "recipe", params, this.http).pipe(
      map(response   => {
        this.recipeCache.set(Object.values(userParam).join('-'), response);
        return response;
      })
    );
  }
  getRecipe(id: number) {
    const recipe = [...this.recipeCache.values()]
      .reduce((arr, elm) => arr.concat(elm.result), []).find((recipe: Recipe) => recipe.id == id);
    if (recipe) return of(recipe);
    return this.http.get<Recipe>(this.baseUrl + 'recipe/' + id);
  }
  getRecipeSteps(id: number) {
    return this.http.get<RecipeStep[]>(this.baseUrl + 'recipe/recipe-steps/' + id);
  }
  getTrendingRecipes() {
    return this.http.get<Recipe[]>(this.baseUrl + 'recipe/trending');
  }
  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'recipe/category');
  }
  addRecipe(recipe: AddRecipe) {
    return this.http.post(this.baseUrl + 'recipe/add-recipe', recipe);
  }
  getIngredients() {
    return this.http.get<Ingredient[]>(this.baseUrl + 'recipe/ingredient');
  }
  getUserRecipe() {
    return this.http.get<Recipe[]>(this.baseUrl + 'recipe/user-recipe');
  }
  getUserProfileRecipe(email: string) {
    return this.http.get<Recipe[]>(this.baseUrl + 'recipe/profile-recipe/' + email);
  }
}
