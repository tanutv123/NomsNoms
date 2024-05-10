import { Injectable } from '@angular/core';
import {DecodedToken} from "../_model/decodedToken.model";
import {User} from "../_model/user.model";
import {BehaviorSubject, map} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(
    private http: HttpClient,
    private toastr: ToastrService,
    private router: Router,
  ) {}

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'auth/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }


  getCurrentUser() {
    const userJson = localStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : null;
  }

  setCurrentUser(user: User) {
    const expiry = this.getDecodedToken(user.token).exp;
    const now = Math.floor(new Date().getTime() / 1000);
    if (expiry < now) {
      localStorage.removeItem('user');
      this.router.navigateByUrl('/login');
    } else {
      user.roles = [];
      const roles = this.getDecodedToken(user.token).role;
      Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
    }
  }

  isAdmin() {
    const user = this.getCurrentUser();
    return user?.roles?.includes('Admin');
  }

  isManager() {
    const user = this.getCurrentUser();
    return user?.roles?.includes('Manager');
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getDecodedToken(token: string): DecodedToken {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
