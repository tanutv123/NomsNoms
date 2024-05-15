import { Component } from '@angular/core';
import {faClock, faBolt, faChartSimple} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  openTab = 1;
  toggleTabs($tabNumber: number){
    this.openTab = $tabNumber;

  }
}
