import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {RecipeService} from "../../../_services/recipe.service";
import {Recipe} from "../../../_model/recipe.model";
import {Pagination} from "../../../_model/pagination.model";
import {UserParams} from "../../../_model/userParams.model";

@Component({
  selector: 'app-list-of-recipe',
  templateUrl: './list-of-recipe.component.html',
  styleUrls: ['./list-of-recipe.component.scss']
})
export class ListOfRecipeComponent implements OnInit{
  categoryId = 0;
  recipes: Recipe[] = [];
  pagination: Pagination | undefined;
  userParams: UserParams | undefined;
  constructor(private activedRoute: ActivatedRoute,
              private recipeService: RecipeService) {
    this.userParams = this.recipeService.getUserParams();
  }
  ngOnInit() {
    this.categoryId = +this.activedRoute.snapshot.queryParams['categoryId'];
    if (this.userParams && this.userParams.categoryId) {
      this.userParams.categoryId = this.categoryId;
    }
    this.loadRecipe();
  }

  loadRecipe() {
    if (!this.userParams) return;
    this.recipeService.setUserParams(this.userParams);
    this.recipeService.getRecipes(this.userParams).subscribe({
      next:response => {
        if (response && response.result && response.pagination) {
          this.recipes = response.result;
          this.pagination = response.pagination;
        }
      }
    });
  }
}
