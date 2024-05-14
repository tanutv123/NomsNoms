import {Component, Input} from '@angular/core';
import {faClock, faBolt, faChartSimple} from "@fortawesome/free-solid-svg-icons";
import {Recipe} from "../../../../_model/recipe.model";

@Component({
  selector: 'app-recipe-card-profile',
  templateUrl: './recipe-card-profile.component.html',
  styleUrls: ['./recipe-card-profile.component.scss']
})
export class RecipeCardProfileComponent {
  @Input('recipe') recipe: Recipe | undefined;
  faClockIcon = faClock;
  faBolt = faBolt;
  faChartSimple = faChartSimple;
}
