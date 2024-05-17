import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {User} from "../_model/user.model";
import {UserAdmin} from "../_model/Admin/userAdmin.model";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<UserAdmin[]>(this.baseUrl + 'admin/users');
  }
  deleteUser(user: UserAdmin) {
    return this.http.delete<any>(this.baseUrl + 'admin/users/delete-user', {body: user});
  }
}
