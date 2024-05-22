import { Component } from '@angular/core';
import {TasteProfile} from "../../_model/tasteProfile.model";
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {AccountService} from "../../_services/account.service";

@Component({
  selector: 'app-taste-test',
  templateUrl: './taste-test.component.html',
  styleUrls: ['./taste-test.component.css']
})
export class TasteTestComponent {
  tasteProfile: TasteProfile = {
    spiciness: 0,
    saltiness: 0,
    sweetness: 0,
    sauce: 0
  };
  constructor(private userService: UserService,
              private toastr: ToastrService,
              private accountService: AccountService,
              private router: Router) {
  }
  formatLabel(value: number): string {
    if (value >= 1000) {
      return Math.round(value / 1000) + '';
    }

    return `${value}`;
  }

  submitForm() {
    console.log(this.tasteProfile);
    this.userService.setTasteProfile(this.tasteProfile).subscribe({
      next: _ => {
        this.toastr.success('Thành công. Vui lòng đăng nhập lại để có trải nghiệm tốt nhất');
        this.accountService.logout();
        this.router.navigateByUrl('/login');
      }
    });
  }
}
