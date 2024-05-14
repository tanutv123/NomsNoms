import {AfterViewInit, Component, ElementRef, Input, ViewChild} from '@angular/core';
import {RecipeStep} from "../../../../_model/recipeStep.model";
import {SwiperContainer} from "swiper/swiper-element";
import {SwiperOptions} from "swiper/types";

@Component({
  selector: 'app-steps',
  templateUrl: './steps.component.html',
  styleUrls: ['./steps.component.scss']
})
export class StepsComponent implements AfterViewInit{
  @Input('steps') steps: RecipeStep[] | undefined;
  @ViewChild('swiperContainer') swiperContainerRef!: ElementRef<SwiperContainer>;
  index = 0;
  swiperConfig: SwiperOptions = {
    pagination: {clickable: true, type: 'progressbar'},
    navigation: true,
    watchSlidesProgress: true
  }
  ngAfterViewInit() {
    this.initSwiper();
  }
  private initSwiper(): void {
    this.swiperContainerRef.nativeElement.swiper.activeIndex = this.index;
    setTimeout(() => {
      this.swiperContainerRef.nativeElement.swiper.updateSize();
    }, 1000)
  }

  slideChange(swiper: any) {
    this.index =swiper.detail[0].activeIndex;
  }
}
