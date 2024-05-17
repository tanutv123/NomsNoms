import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {PaymentService} from "../../../_services/payment.service";

@Component({
  selector: 'app-fail',
  templateUrl: './fail.component.html',
  styleUrls: ['./fail.component.css']
})
export class FailComponent implements OnInit{

  constructor(private route: ActivatedRoute,
              private paymentService: PaymentService) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe({
      next: queryParams => {
        this.paymentService.verifyPayment(queryParams['orderCode']).subscribe({
          next: response => {
            console.log(response);
          }
        });
      }
    });
  }

}
