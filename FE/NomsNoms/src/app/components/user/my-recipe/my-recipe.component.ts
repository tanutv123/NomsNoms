import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {RecipeService} from "../../../_services/recipe.service";
import {Subject} from "rxjs";

@Component({
  selector: 'app-my-recipe',
  templateUrl: './my-recipe.component.html',
  styleUrls: ['./my-recipe.component.scss']
})
export class MyRecipeComponent implements OnInit{
  recipes: Recipe[] | undefined;
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  constructor(private recipeService: RecipeService) {
  }
  ngOnInit(): void {
    this.loadUserRecipe();
  }
  loadUserRecipe() {
    this.recipeService.getUserRecipe().subscribe({
      next: recipes => {
        if (recipes) {
          this.recipes = recipes;
          this.dtTrigger.next(null);
        }
      }
    });
  }

}
