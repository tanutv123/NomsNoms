import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import Swiper from "swiper";
import {ActivatedRoute} from "@angular/router";
import {RecipeService} from "../../../_services/recipe.service";
import {RecipeStep} from "../../../_model/recipeStep.model";
import {SwiperContainer} from "swiper/swiper-element";
import {SwiperOptions} from "swiper/types";
@Component({
  selector: 'app-recipe-step-list',
  templateUrl: './recipe-step-list.component.html',
  styleUrls: ['./recipe-step-list.component.scss']
})
export class RecipeStepListComponent implements OnInit, AfterViewInit{
  id = 0;
  steps : RecipeStep[] = [];
  @ViewChild('swiperContainer') swiperContainerRef!: ElementRef<SwiperContainer>;

  constructor(private route: ActivatedRoute,
              private recipeService: RecipeService) {

  }

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id')!;
    this.recipeService.getRecipeSteps(this.id).subscribe({
      next: steps => {
        this.steps = steps;
      }
    });
  }

  ngAfterViewInit() {
  }
}
