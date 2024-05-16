import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../../../environments/environment";

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent {
  validationErrors: string[] = [];
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {
  }

  get404Error() {
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
      next: response => console.log(response),
      error: err => console.log(err)
    });
  }
  get400Error() {
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
      next: response => console.log(response),
      error: err => console.log(err)
    });
  }
  get500Error() {
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
      next: response => console.log(response),
      error: err => console.log(err)
    });
  }
  get401Error() {
    this.http.get(this.baseUrl + 'buggy/auth').subscribe({
      next: response => console.log(response),
      error: err => console.log(err)
    });
  }
  get400ValidationError() {
    this.http.post(this.baseUrl + 'auth/register', {}).subscribe({
      next: response => console.log(response),
      error: err => this.validationErrors = err
    });
  }
}
