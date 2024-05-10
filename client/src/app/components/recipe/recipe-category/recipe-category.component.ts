import {Component, Input} from '@angular/core';
import {Category} from "../../../_model/category.model";
import {Router} from "@angular/router";

@Component({
  selector: 'app-recipe-category',
  templateUrl: './recipe-category.component.html',
  styleUrls: ['./recipe-category.component.scss']
})
export class RecipeCategoryComponent {
  @Input('category') category : Category | undefined;
  constructor(private router: Router) {
  }

  goToRecipeList() {
    this.router.navigate(['/list'], {queryParams: {categoryId: this.category?.id}});
  }
}
