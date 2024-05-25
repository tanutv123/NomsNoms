import { Component } from '@angular/core';
import {Ingredient} from "../../../../_model/ingredient.model";
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-ingredient-create',
  templateUrl: './ingredient-create.component.html',
  styleUrls: ['./ingredient-create.component.css']
})
export class IngredientCreateComponent {
  ingredient: Ingredient = {} as Ingredient;
  validationErrors: string[] = [];

  constructor(private adminService: RecipeAdminService,
              private router: Router,
              private toastr: ToastrService) {
  }

  createIngredient(){
    this.adminService.createIngredient(this.ingredient).subscribe({
      next: _ => {
        this.router.navigateByUrl('ingredients');
        this.toastr.success('Tạo thành công');
      }
    });
  }
}
