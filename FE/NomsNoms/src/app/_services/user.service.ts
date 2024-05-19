import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {User} from "../_model/user.model";
import {UserAdmin} from "../_model/Admin/userAdmin.model";
import {TopFollow} from "../_model/topFollow.model";
import {SubscriptionModel} from "../_model/subscription.model";

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
  enable(user: UserAdmin) {
    return this.http.put<any>(this.baseUrl + 'admin/users/enable-user/' + user.email, null);
  }

  getTopUser() {
    return this.http.get<TopFollow[]>(this.baseUrl + 'user/top10');
  }

  getUserSubscription(email: string) {
    return this.http.get<SubscriptionModel>(this.baseUrl + 'user/subscription/' + email);
  }

  updateUserSubscription(model: any) {
    return this.http.put(this.baseUrl + 'user/update-subscription', model);
  }
}
