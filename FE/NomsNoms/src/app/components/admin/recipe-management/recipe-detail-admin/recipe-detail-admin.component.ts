import {Component, OnInit} from '@angular/core';
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";
import {RecipeAdmin} from "../../../../_model/Admin/recipeAdmin.model";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-recipe-detail-admin',
  templateUrl: './recipe-detail-admin.component.html',
  styleUrls: ['./recipe-detail-admin.component.css']
})
export class RecipeDetailAdminComponent implements OnInit{
  recipe: RecipeAdmin | undefined;
  constructor(private recipeAdminService: RecipeAdminService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.recipe = data['recipe'];
      }
    });
  }
}
