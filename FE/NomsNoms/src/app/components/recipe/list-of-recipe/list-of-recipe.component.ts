import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {RecipeService} from "../../../_services/recipe.service";
import {Recipe} from "../../../_model/recipe.model";
import {Pagination} from "../../../_model/pagination.model";
import {UserParams} from "../../../_model/userParams.model";
import {Category} from "../../../_model/category.model";

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
  categories: Category[] = [];
  selectedCategories: any[] = [];
  orderByTime = [
    {value: 'asc', display: 'Thời gian thực hiện tăng dần'},
    {value: 'desc', display: 'Thời gian thực hiện giảm dần'}
  ];
  orderByComplexity = [
    {value: 'asc', display: 'Độ khó tăng dần'},
    {value: 'desc', display: 'Độ khó giảm dần'}
  ];
  constructor(private activedRoute: ActivatedRoute,
              private recipeService: RecipeService) {
    this.userParams = this.recipeService.getUserParams();
  }
  ngOnInit() {
    this.categoryId = +this.activedRoute.snapshot.queryParams['categoryId'];
    if (this.categoryId)
    this.selectedCategories.push(this.categoryId);
    if (this.userParams && this.userParams.categoryIds) {
      this.userParams.categoryIds = this.selectedCategories;
    }
    this.loadRecipe();
    this.loadCategories();
  }
  loadCategories() {
    this.recipeService.getCategories().subscribe({
      next: categories => {
        this.categories = categories;
      }
    })
  }
  loadRecipe() {
    if (!this.userParams) return;
    this.userParams.categoryIds = this.selectedCategories;
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

  onCategorySelected(categoryId: number) {
    const index = this.selectedCategories.indexOf(categoryId);
    index !== -1 ? this.selectedCategories.splice(index, 1) : this.selectedCategories.push(categoryId);
    console.log(this.selectedCategories);
    this.loadRecipe();
  }
  isSelectedCategory(categoryId: number) {
    return this.selectedCategories.some(x => x == categoryId);
  }
  pageChanged(event : any) {
    if (this.userParams && this.userParams?.pageNumber != event.page) {
      this.userParams.pageNumber = event.page;
      this.recipeService.setUserParams(this.userParams);
      this.loadRecipe();
    }
  }
}
