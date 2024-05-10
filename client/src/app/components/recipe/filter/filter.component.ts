import {Component, OnInit} from '@angular/core';
import {RecipeService} from "../../../_services/recipe.service";
import {Category} from "../../../_model/category.model";

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit{
  categories : Category[] = [];
  constructor(private recipeService: RecipeService) {
  }

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.recipeService.getCategories().subscribe({
      next: categories => {
        this.categories = categories;
      }
    });
  }

}
