import {Component, OnInit} from '@angular/core';
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";
import {RecipeAdmin} from "../../../../_model/Admin/recipeAdmin.model";
import {ActivatedRoute, Router} from "@angular/router";
import {TasteProfile} from "../../../../_model/tasteProfile.model";
import {ToastrService} from "ngx-toastr";
import {Recipe} from "../../../../_model/recipe.model";

@Component({
  selector: 'app-recipe-detail-admin',
  templateUrl: './recipe-detail-admin.component.html',
  styleUrls: ['./recipe-detail-admin.component.css']
})
export class RecipeDetailAdminComponent implements OnInit{
  recipe: RecipeAdmin | undefined;
  tasteProfile: TasteProfile = {
    spiciness: 0,
    saltiness: 0,
    sweetness: 0,
    sauce: 0
  };
  formatLabel(value: number): string {
    if (value >= 1000) {
      return Math.round(value / 1000) + '';
    }

    return `${value}`;
  }
  constructor(private recipeAdminService: RecipeAdminService,
              private route: ActivatedRoute,
              private toastr: ToastrService,
              private router: Router) {
  }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.recipe = data['recipe'];
      }
    });
  }
  submitForm() {
    if (!this.recipe) return;
    if (this.tasteProfile.spiciness == 0 && this.tasteProfile.saltiness == 0 && this.tasteProfile.sweetness == 0 && this.tasteProfile.sauce == 0) {
      this.toastr.error('Cập nhật khẩu vị trước khi duyệt');
      return;
    }
    this.recipeAdminService.acceptRecipe(this.recipe.id, this.tasteProfile).subscribe({
      next: _ => {
        this.toastr.success('Duyệt thành công');
        this.router.navigateByUrl('/pendings');
      }
    });
  }
  deleteRecipe() {
    if (!this.recipe) return;
    var recipe1: Recipe = {
      id : this.recipe.id,
    } as Recipe;
    this.recipeAdminService.deleteRecipe(recipe1).subscribe({
      next: _ => {
        this.toastr.success('Xóa thành công');
        this.router.navigateByUrl('/pendings');
      }
    });
  }
}
