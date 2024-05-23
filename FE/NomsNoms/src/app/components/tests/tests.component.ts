import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormControl, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInput, MatInputModule} from "@angular/material/input";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {NgFor} from "@angular/common";
import {async, map, Observable, startWith} from "rxjs";
import {Ingredient} from "../../_model/ingredient.model";
import {RecipeService} from "../../_services/recipe.service";

@Component({
  selector: 'app-tests',
  templateUrl: './tests.component.html',
  styleUrls: ['./tests.component.css'],
})
export class TestsComponent implements OnInit{
  ingredients: Ingredient[] = [];
  selectedIngredient: number;
  constructor(private recipeService: RecipeService) {
  }

  ngOnInit() {
    this.loadIngredients();
  }

  loadIngredients() {
    this.recipeService.getIngredients().subscribe({
      next: res => {
        this.ingredients = res;
      }
    });
  }
}
