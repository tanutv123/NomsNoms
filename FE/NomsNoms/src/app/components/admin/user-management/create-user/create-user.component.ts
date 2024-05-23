import { Component } from '@angular/core';
import {Register} from "../../../../_model/register.model";
import {FormBuilder, Validators} from "@angular/forms";
import {AccountService} from "../../../../_services/account.service";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css']
})
export class CreateUserComponent {
  validationErrors: string[] = [];
  user: Register = {} as Register;
  constructor(private fb: FormBuilder,
              private adminService: RecipeAdminService,
              private router: Router,
              private toastr: ToastrService) {
  }
  complexPassword = '(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;\'?/&gt;.&lt;,])(?!.*\\s).*$';

  onSubmit() {
    this.adminService.createUser(this.user).subscribe({
      next: user => {
        this.router.navigateByUrl('/create-user')
        this.toastr.success("Tạo thành công");
      },
      error: errs => this.validationErrors = errs
    });
  }
}
