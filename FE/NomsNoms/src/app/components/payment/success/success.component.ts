import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {PaymentService} from "../../../_services/payment.service";
import {AccountService} from "../../../_services/account.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.css']
})
export class SuccessComponent implements OnInit{
  constructor(private route: ActivatedRoute,
              private paymentService: PaymentService,
              private accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe({
      next: queryParams => {
        this.paymentService.verifyPayment(queryParams['orderCode']).subscribe({
          next: response => {
            this.accountService.logout();
            this.router.navigateByUrl('/login');
            this.toastr.success('Hãy đăng nhập lại để trải nghiệm dịch vụ bạn vừa mua');
          }
        });
      }
    });
  }
}
