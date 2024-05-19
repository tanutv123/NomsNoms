import {Component, OnInit} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {Subject} from "rxjs";
import {RecipeAdmin} from "../../../_model/Admin/recipeAdmin.model";
import {RecipeAdminService} from "../../../_services/recipe-admin.service";

@Component({
  selector: 'app-recipe-management',
  templateUrl: './recipe-management.component.html',
  styleUrls: ['./recipe-management.component.css']
})
export class RecipeManagementComponent implements OnInit{

  recipes: Recipe[] | undefined;
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  constructor(private recipeAdminService: RecipeAdminService) {
  }

  ngOnInit() {
    this.loadRecipes();
  }

  loadRecipes() {
    this.recipeAdminService.getRecipes().subscribe({
      next: recipes => {
        this.recipes = recipes;
        this.dtTrigger.next(null);
      }
    });
  }
}
