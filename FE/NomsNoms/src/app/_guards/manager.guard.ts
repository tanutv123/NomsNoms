import { CanActivateFn } from '@angular/router';
import {inject} from "@angular/core";
import {AccountService} from "../_services/account.service";
import {ToastrService} from "ngx-toastr";
import {map} from "rxjs";

export const managerGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  return accountService.currentUser$.pipe(
    map(user => {
      if (!user) return false;
      if (user.roles == 'Manager') {
        return true;
      } else {
        toastr.error('Bạn không có quyền truy cập');
        return false;
      }
    })
  );
};
