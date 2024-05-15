import {Component, OnInit} from '@angular/core';
import {RecipeService} from "../../_services/recipe.service";
import {Recipe} from "../../_model/recipe.model";

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements OnInit{
  recipes: Recipe[] = [];
  constructor(private recipeService: RecipeService) {
  }

  ngOnInit(): void {
    this.recipeService.getTrendingRecipes().subscribe({
      next: recipes => {
        this.recipes = recipes;
      }
    });
  }


}
