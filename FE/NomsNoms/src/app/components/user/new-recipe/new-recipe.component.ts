import {Component, OnInit} from '@angular/core';
import {Image} from "../../../_model/image.model";
import {AddRecipe} from "../../../_model/AddRecipe/addRecipe.model";
import {Category} from "../../../_model/category.model";
import {AddRecipeStep} from "../../../_model/AddRecipe/addRecipeStep.model";
import {RecipeService} from "../../../_services/recipe.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {Ingredient} from "../../../_model/ingredient.model";
import {AddRecipeCategory} from "../../../_model/AddRecipe/addRecipeCategory.model";
import {AddRecipeIngredient} from "../../../_model/AddRecipe/AddRecipeIngredient.model";
import {User} from "../../../_model/user.model";
import {AccountService} from "../../../_services/account.service";
import {take} from "rxjs";

@Component({
  selector: 'app-new-recipe',
  templateUrl: './new-recipe.component.html',
  styleUrls: ['./new-recipe.component.scss']
})
export class NewRecipeComponent implements OnInit{
  recipe: AddRecipe  = {} as AddRecipe;
  recipeStep: AddRecipeStep = {} as AddRecipeStep;
  categories: Category[] = [];
  ingredients: Ingredient[] = [];
  addedCategories: Category[] = [];
  validationErrors: string[] = [];
  selectedCategory: AddRecipeCategory = {} as AddRecipeCategory;
  selectedIngredientId: number;
  selectedIngredient: AddRecipeIngredient = {} as AddRecipeIngredient;
  addedIngredients: Ingredient[] = [];
  user: User | undefined;
  hasSubscription = false;
  constructor(private recipeService: RecipeService,
              private toastr: ToastrService,
              private accountService: AccountService,
              private router: Router) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
          this.hasSubscription = this.user.subscriptionId != 0;
        }
      }
    });
  }

  ngOnInit() {
    this.recipe.recipeSteps = [];
    this.recipeStep.recipeStepImages = [];
    this.recipe.recipeIngredients = [];
    this.recipe.recipeCategories = [];
    this.recipe.recipeComplexityId = 1;
    this.loadCategories();
    this.loadIngredients();
  }
  loadCategories() {
    this.recipeService.getCategories().subscribe({
      next: categories => {
        this.categories = categories;
      }
    });
  }
  loadIngredients() {
    this.recipeService.getIngredients().subscribe({
      next: ingredients => {
        this.ingredients = ingredients;
      }
    });
  }

  onAddCategory() {
    if (this.recipe.recipeCategories.length == 0) {
      this.recipe.recipeCategories.push(this.selectedCategory);
      // this.recipe.recipeCategories = [...this.recipe.recipeCategories, this.selectedCategory];
      let category = this.categories.find(x => x.id == this.selectedCategory.categoryId);
      if (category)
        this.addedCategories.push(category);
    }
    else {
      if (this.recipe.recipeCategories.some(x => x.categoryId == this.selectedCategory.categoryId)) {
        this.toastr.error("Thể loại này đã được thêm")
      }
      else {
        this.recipe.recipeCategories.push({categoryId: this.selectedCategory.categoryId});
        // this.recipe.recipeCategories = [...this.recipe.recipeCategories, this.selectedCategory];
        let category1 = this.categories.find(x => x.id == this.selectedCategory.categoryId);
        if (category1)
          this.addedCategories.push(category1);
      }
    }
    this.selectedCategory = {} as AddRecipeCategory;
    console.log(this.recipe.recipeCategories);
  }
  onDeleteCategory(categoryId: number) {
    this.addedCategories = this.addedCategories.filter(x => x.id !== categoryId);
    let index = this.recipe.recipeCategories.findIndex(x => x.categoryId !== categoryId);
    this.recipe.recipeCategories.splice(index, 1);
    console.log(this.recipe.recipeCategories);
  }
  onAddIngredient() {
    if (this.recipe.recipeIngredients.length == 0) {
      this.recipe.recipeIngredients.push(this.selectedIngredient);
      // this.recipe.recipeCategories = [...this.recipe.recipeCategories, this.selectedCategory];
      let ingredient = this.ingredients.find(x => x.id == this.selectedIngredient.ingredientId);
      if (ingredient) {
        ingredient.serving = this.selectedIngredient.ingredientServing;
        this.addedIngredients.push(ingredient);
      }

    }
    else {
      if (this.recipe.recipeIngredients.some(x => x.ingredientId == this.selectedIngredient.ingredientId)) {
        this.toastr.error("Nguyên liệu này đã được thêm")
      }
      else {
        this.recipe.recipeIngredients.push(this.selectedIngredient);
        // this.recipe.recipeCategories = [...this.recipe.recipeCategories, this.selectedCategory];
        let ingredient1 = this.ingredients.find(x => x.id == this.selectedIngredient.ingredientId);
        if (ingredient1) {
          ingredient1.serving = this.selectedIngredient.ingredientServing;
          this.addedIngredients.push(ingredient1);
        }
      }
    }
    this.selectedIngredient = {} as AddRecipeIngredient;
    console.log(this.recipe.recipeIngredients);
  }
  onDeleteIngredient(ingredientId: number) {
    this.addedIngredients = this.addedIngredients.filter(x => x.id !== ingredientId);
    let index = this.recipe.recipeIngredients.findIndex(x => x.ingredientId !== ingredientId);
    this.recipe.recipeIngredients.splice(index, 1);
    console.log(this.recipe.recipeIngredients);
  }
  onImageAdded(imageData: Image) {
    this.recipeStep.recipeStepImages.push(imageData);
  }
  onRecipeImageAdded(imageData: Image) {
    this.recipe.recipeImage = imageData;
  }

  onAddStep() {
    if (this.recipe.recipeSteps) {
      if (this.recipe.recipeSteps.length == 0) {
        this.recipeStep.no = 1;
      }
      else {
        this.recipeStep.no = this.recipe.recipeSteps.length + 1;
      }
    }
    this.recipe.recipeSteps.push(this.recipeStep);
    this.recipeStep = {} as AddRecipeStep;
    this.recipeStep.recipeStepImages = [];
  }

  onAddRecipe() {
    if (this.recipe.recipeIngredients.length <= 0) {
      this.toastr.error("Hãy thêm nguyên liệu cho món ăn của bạn");
    }
    else if (this.recipe.recipeCategories.length <= 0) {
      this.toastr.error("Hãy thêm thể loại món ăn");
    }
    else if (!this.recipe.recipeImage) {
      this.toastr.error("Hãy thêm hình minh họa món ăn của bạn");
    }
    else if (this.recipe.recipeSteps.length <= 0) {
      this.toastr.error("Hãy thêm các bước thực hiện món ăn");
    }
    else {
      this.recipeService.addRecipe(this.recipe).subscribe({
        next : _ => {
          this.toastr.success("Thêm công thức thành công");
          this.router.navigateByUrl('/my-recipe');
        },
        error: err => this.validationErrors = err
      });
    }
  }
}
