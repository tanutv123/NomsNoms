import {AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {Recipe} from "../../../_model/recipe.model";
import {SwiperContainer} from "swiper/swiper-element";
import {SwiperOptions} from "swiper/types";
import {faBolt, faChartSimple, faClock} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-trending',
  templateUrl: './trending.component.html',
  styleUrls: ['./trending.component.scss']
})
export class TrendingComponent implements OnInit, AfterViewInit{
  @Input('recipes') recipes: Recipe[] = [];
  @ViewChild('swiperContainer') swiperContainerRef!: ElementRef<SwiperContainer>;
  index = 0;
  swiperConfig: SwiperOptions = {
    pagination: {clickable: true},
    navigation: true,
    watchSlidesProgress: true,
    spaceBetween: 30,
    slidesPerView: 3
  }
  faClockIcon = faClock;
  faBolt1 = faBolt;
  faChartSimple1 = faChartSimple;
  constructor() {
  }



  ngOnInit() {
  }
  ngAfterViewInit(): void {
    this.initSwiper();
  }
  private initSwiper(): void {
    this.swiperContainerRef.nativeElement.swiper.activeIndex = this.index;
    setTimeout(() => {
      this.swiperContainerRef.nativeElement.swiper.updateSize();
    }, 1000);
  }
  slideChange(swiper: any) {
    this.index =swiper.detail[0].activeIndex;
  }

  protected readonly faBolt = faBolt;
  protected readonly faClock = faClock;
  protected readonly faChartSimple = faChartSimple;
}
