import {Component, OnInit} from '@angular/core';
import {User} from "./_model/user.model";
import {AccountService} from "./_services/account.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'NomsNoms';

  constructor(public accountService: AccountService) {
  }

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);

  }
}
