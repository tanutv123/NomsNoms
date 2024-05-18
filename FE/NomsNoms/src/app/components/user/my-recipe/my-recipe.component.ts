import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {RecipeService} from "../../../_services/recipe.service";
import {Subject} from "rxjs";
import {ToastrService} from "ngx-toastr";

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
  @ViewChild('recipeTable') recipeTable: ElementRef;
  constructor(private recipeService: RecipeService,
              private toastr: ToastrService,
              private renderer: Renderer2) {
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

  hideRecipe(recipeId: number) {
    this.recipeService.hideRecipe(recipeId).subscribe({
      next: _ => {
        var recipe = this.recipes?.find(x => x.id == recipeId);
        if (recipe) {
          var status = recipe.recipeStatusName;
          var statusToChange = '';
          if (status == 'Hidden') {
            statusToChange = 'Normal'
            this.toastr.success("Hiện công thức thành công");
          }
          else {
            statusToChange = 'Hidden'
            this.toastr.success("Ẩn công thức thành công");
          }
          recipe.recipeStatusName = statusToChange;

          const table = this.recipeTable.nativeElement;
          const row = table.querySelector(`[data-recipe-id="${recipeId}"]`);

          if (row) {
            const cell = row.cells[1];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', statusToChange);
          }
        }
      }
    });
  }
  deleteRecipe(recipeId: number) {
    this.recipeService.deleteRecipe(recipeId).subscribe({
      next: _ => {
        this.loadUserRecipe();
        this.toastr.success("Xóa công thức thành công");
      }
    });
  }

}
