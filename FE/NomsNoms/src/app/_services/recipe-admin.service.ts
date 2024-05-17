import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {RecipeAdmin} from "../_model/Admin/recipeAdmin.model";

@Injectable({
  providedIn: 'root'
})
export class RecipeAdminService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getRecipe(id: number) {
    return this.http.get<RecipeAdmin>(this.baseUrl + 'admin/recipe/' + id);
  }
}
