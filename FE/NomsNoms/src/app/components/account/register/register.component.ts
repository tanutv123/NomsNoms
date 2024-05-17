import { Component } from '@angular/core';
import {FormBuilder, Validators} from "@angular/forms";
import {User} from "../../../_model/user.model";
import {Register} from "../../../_model/register.model";
import {AccountService} from "../../../_services/account.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  validationErrors: string[] = [];
  user: Register = {} as Register;
  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) {
  }
  complexPassword = '(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;\'?/&gt;.&lt;,])(?!.*\\s).*$';
  registerForm = this.fb.group({
    displayName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required,  Validators.pattern(this.complexPassword)]],
  });

  onSubmit() {
    this.accountService.register(this.user).subscribe({
      next: user => {
        this.accountService.setCurrentUser(user);
        this.router.navigateByUrl('/')
        this.toastr.success("Đăng ký thành công");
      },
      error: errs => this.validationErrors = errs
    });
  }

}
