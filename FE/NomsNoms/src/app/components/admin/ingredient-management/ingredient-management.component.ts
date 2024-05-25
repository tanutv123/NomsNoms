import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {Subject} from "rxjs";
import {Ingredient} from "../../../_model/ingredient.model";
import {RecipeAdminService} from "../../../_services/recipe-admin.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-ingredient-management',
  templateUrl: './ingredient-management.component.html',
  styleUrls: ['./ingredient-management.component.css']
})
export class IngredientManagementComponent implements OnInit{
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  @ViewChild('ingredientTable') ingredientTable: ElementRef;
  ingredients: Ingredient[] = [];

  constructor(private adminService: RecipeAdminService,
              private renderer: Renderer2,
              private toastr: ToastrService) {
  }

  ngOnInit() {
    this.adminService.getIngredients().subscribe({
      next: res => {
        this.ingredients = res;
        this.dtTrigger.next(null);
      }
    });
  }

  enableIngredient(ingredient: Ingredient){
    this.adminService.enableIngredients(ingredient.id).subscribe({
      next: _ => {
        if (this.ingredients)
          var user1 = this.ingredients.find(x => x.id == ingredient.id);
        if (user1) {
          user1.status = 1;

          const table = this.ingredientTable.nativeElement;
          const row = table.querySelector(`[data-ingredient-id="${ingredient.id}"]`);

          if (row) {
            const cell = row.cells[5];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', "Hoạt động");
          }
          this.toastr.success("Mở thành công");
        }
      }
    });
  }

  disableIngredient(ingredient: Ingredient){
    this.adminService.deleteIngredients(ingredient.id).subscribe({
      next: _ => {
        if (this.ingredients)
          var user1 = this.ingredients.find(x => x.id == ingredient.id);
        if (user1) {
          user1.status = 0;

          const table = this.ingredientTable.nativeElement;
          const row = table.querySelector(`[data-ingredient-id="${ingredient.id}"]`);

          if (row) {
            const cell = row.cells[5];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', "Vô hiệu hóa");
          }
          this.toastr.success("Vô hiệu hóa thành công");
        }
      }
    });
  }

}
