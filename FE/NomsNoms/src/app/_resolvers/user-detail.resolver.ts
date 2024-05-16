import { ResolveFn } from '@angular/router';
import {inject} from "@angular/core";
import {AccountService} from "../_services/account.service";
import {User} from "../_model/user.model";

export const userDetailResolver: ResolveFn<User> = (route, state) => {
  const accountService = inject(AccountService);
  return accountService.getUserProfile(route.paramMap.get('email')!);
};
