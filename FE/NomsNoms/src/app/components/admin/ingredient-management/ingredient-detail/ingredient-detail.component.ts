import {Component, OnInit} from '@angular/core';
import {Ingredient} from "../../../../_model/ingredient.model";
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";
import {ToastrService} from "ngx-toastr";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-ingredient-detail',
  templateUrl: './ingredient-detail.component.html',
  styleUrls: ['./ingredient-detail.component.css']
})
export class IngredientDetailComponent implements OnInit{
  ingredient: Ingredient | undefined;
  validationErrors: string[] = [];

  constructor(private adminService: RecipeAdminService,
              private toastr: ToastrService,
              private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.ingredient = data['ingredient'];
      }
    });
  }

  updateIngredient(){
    if (!this.ingredient) return;
    this.adminService.updateIngredient(this.ingredient).subscribe({
      next: _ => {
        this.toastr.success('Cập nhật thành công');
      }
    });
  }

}
