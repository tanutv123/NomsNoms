import {Component, HostListener} from '@angular/core';
import {AccountService} from "../../_services/account.service";


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  showDropdown = false;
  constructor(public accountService: AccountService) {
  }

  logout() {
    this.accountService.logout();
  }
}
