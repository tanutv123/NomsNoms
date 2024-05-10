import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit{
  recipe: Recipe | undefined;
  constructor(private route: ActivatedRoute,
              private router: Router) {
  }
  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.recipe = data['recipe'];
      }
    });
  }
}
