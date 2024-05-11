import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import Swiper from "swiper";
import {ActivatedRoute} from "@angular/router";
import {RecipeService} from "../../../_services/recipe.service";
import {RecipeStep} from "../../../_model/recipeStep.model";
@Component({
  selector: 'app-recipe-step-list',
  templateUrl: './recipe-step-list.component.html',
  styleUrls: ['./recipe-step-list.component.scss']
})
export class RecipeStepListComponent implements OnInit, AfterViewInit{
  id = 0;
  steps : RecipeStep[] = [];
  @ViewChild('swiperContainer', { static: false }) swiperContainerRef?: ElementRef;

  constructor(private route: ActivatedRoute,
              private recipeService: RecipeService) {

  }

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id')!;
    this.recipeService.getRecipeSteps(this.id).subscribe({
      next: steps => {
        this.steps = steps;
        console.log(this.steps);
      }
    });
  }

  ngAfterViewInit() {
    this.initSwiper();
  }
  private initSwiper(): void {
    const mySwiper = new Swiper('.mySwiper', {
      // Swiper configuration options
      pagination: {
        el: '.swiper-pagination',
      },
      navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
      },
    });
  }
}
