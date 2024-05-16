import {Directive, Input, TemplateRef, ViewContainerRef} from '@angular/core';
import {User} from "../_model/user.model";
import {AccountService} from "../_services/account.service";
import {take} from "rxjs";

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective {
  @Input() appHasRole: string = '';
  user: User = {} as User;
  constructor(private viewContainerRef: ViewContainerRef,
              private templateRef: TemplateRef<any>,
              private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    })
  }

  ngOnInit() {
    if (this.user.roles == this.appHasRole) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }

}
