import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {ActivatedRoute, Router} from "@angular/router";
import {RecipeService} from "../../../_services/recipe.service";

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit{
  recipe: Recipe | undefined;
  isLiked = false;
  constructor(private route: ActivatedRoute,
              private router: Router,
              private recipeService: RecipeService) {
  }
  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.recipe = data['recipe'];
      }
    });
    this.checkLike();
  }

  checkLike() {
    if (!this.recipe) return;
    this.recipeService.isLikeRecipe(this.recipe.id).subscribe({
      next: result => {
        this.isLiked = result;
      }
    });
  }
  like() {
    if (!this.recipe) return;
    this.recipeService.likeRecipe(this.recipe.id).subscribe({
      next: _ => {
        if (this.recipe) {
          if (this.isLiked) {
            this.recipe.numberOfLikes--;
          } else {
            this.recipe.numberOfLikes++;
          }
          this.isLiked = !this.isLiked;
        }
      }
    });
  }
}
