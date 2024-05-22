import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../../../_model/recipe.model";
import {RecipeService} from "../../../../_services/recipe.service";

@Component({
  selector: 'app-recommend-list',
  templateUrl: './recommend-list.component.html',
  styleUrls: ['./recommend-list.component.css']
})
export class RecommendListComponent implements OnInit{
  recipes: Recipe[] = [];

  constructor(private recipeService: RecipeService) {
  }

  ngOnInit() {
    this.recipeService.getRecommendRecipes().subscribe({
      next: res => {
        this.recipes = res;
      }
    });
  }
}
