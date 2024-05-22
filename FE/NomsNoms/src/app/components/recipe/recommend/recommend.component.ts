import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {RecipeService} from "../../../_services/recipe.service";

@Component({
  selector: 'app-recommend',
  templateUrl: './recommend.component.html',
  styleUrls: ['./recommend.component.scss']
})
export class RecommendComponent implements OnInit{
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
