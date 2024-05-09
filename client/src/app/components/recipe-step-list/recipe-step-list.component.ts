import {AfterViewInit, Component, ElementRef, ViewChild} from '@angular/core';
import Swiper from "swiper";

@Component({
  selector: 'app-recipe-step-list',
  templateUrl: './recipe-step-list.component.html',
  styleUrls: ['./recipe-step-list.component.scss']
})
export class RecipeStepListComponent implements AfterViewInit{
  @ViewChild("swiperContainer") swiperContainer: ElementRef | undefined;

  ngAfterViewInit() {
    this.initSwiper();
  }

  private initSwiper() {
    if (!this.swiperContainer) return;
    return new Swiper(this.swiperContainer.nativeElement, {
      slidesPerView:3,
      slidesPerGroup:2,
      centeredSlides: true,
      loop: true
    });
  }
}
