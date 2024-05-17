import {Component, OnInit} from '@angular/core';
import {RecipeService} from "../../_services/recipe.service";
import {Recipe} from "../../_model/recipe.model";
import {UserService} from "../../_services/user.service";
import {TopFollow} from "../../_model/topFollow.model";

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements OnInit{
  recipes: Recipe[] = [];
  topFollow: TopFollow[] = [];
  constructor(private recipeService: RecipeService,
              private userService: UserService) {
  }

  ngOnInit(): void {
    this.recipeService.getTrendingRecipes().subscribe({
      next: recipes => {
        this.recipes = recipes;
      }
    });
    this.loadTopUser();
  }

  loadTopUser() {
    this.userService.getTopUser().subscribe({
      next: r => {
        this.topFollow = r;
      }
    });
  }

}
