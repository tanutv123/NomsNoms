import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {Subject} from "rxjs";
import {RecipeAdmin} from "../../../_model/Admin/recipeAdmin.model";
import {RecipeAdminService} from "../../../_services/recipe-admin.service";
import {ToastrService} from "ngx-toastr";

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
  @ViewChild('recipeTable') recipeTable: ElementRef;
  constructor(private recipeAdminService: RecipeAdminService,
              private renderer: Renderer2,
              private toastr: ToastrService) {
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

  deleteRecipe(recipe1: Recipe) {
    this.recipeAdminService.deleteRecipe(recipe1).subscribe({
      next: _ => {
        var recipe = this.recipes?.find(x => x.id == recipe1.id);
        if (recipe) {
          var statusToChange = 'Deleted';
          this.toastr.success("Đã xóa công thức");
          recipe.recipeStatusName = statusToChange;

          const table = this.recipeTable.nativeElement;
          const row = table.querySelector(`[data-recipe-id="${recipe.id}"]`);

          if (row) {
            const cell = row.cells[1];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', statusToChange);
          }
        }
      }
    });
  }
}
