import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../../../_services/account.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{
  loginForm: FormGroup = new FormGroup({});
  validationErrs: string[] = [];
  returnUrl = '';
  constructor(private fb: FormBuilder,
              private accountService: AccountService,
              private router: Router) {
  }
  ngOnInit() {
    this.initializeLoginForm()
  }

  initializeLoginForm() {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }
  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next:_ =>this.router.navigateByUrl('/'),
      error: err => this.validationErrs = err
    });
  }
}
