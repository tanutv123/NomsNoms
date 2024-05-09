import { Component } from '@angular/core';
import {faClock, faBolt, faChartSimple} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-recipe-card-profile',
  templateUrl: './recipe-card-profile.component.html',
  styleUrls: ['./recipe-card-profile.component.scss']
})
export class RecipeCardProfileComponent {
  faClockIcon = faClock;
  faBolt = faBolt;
  faChartSimple = faChartSimple;
}
