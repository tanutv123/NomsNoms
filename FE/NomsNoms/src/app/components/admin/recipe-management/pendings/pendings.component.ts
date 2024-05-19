import { Component } from '@angular/core';
import {Recipe} from "../../../../_model/recipe.model";
import {Subject} from "rxjs";
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";

@Component({
  selector: 'app-pendings',
  templateUrl: './pendings.component.html',
  styleUrls: ['./pendings.component.css']
})
export class PendingsComponent {
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
    this.recipeAdminService.getPendingRecipes().subscribe({
      next: recipes => {
        this.recipes = recipes;
        this.dtTrigger.next(null);
      }
    });
  }
}
