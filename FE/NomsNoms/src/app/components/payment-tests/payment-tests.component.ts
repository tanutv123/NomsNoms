import { Component } from '@angular/core';
import {PaymentService} from "../../_services/payment.service";
import {CreatePaymentLinkRequest} from "../../_model/createPaymentLinkRequest.model";

@Component({
  selector: 'app-payment-tests',
  templateUrl: './payment-tests.component.html',
  styleUrls: ['./payment-tests.component.css']
})
export class PaymentTestsComponent {
  createPaymentLinkRequest: CreatePaymentLinkRequest = {
    productName: "Mì hảo hảo",
    description: "Thơm",
    price: 2000,
    mealPlanId: 1,
    returnUrl: "http://localhost:4200/payment-success",
    cancelUrl: "http://localhost:4200/payment-fail"
  };
  constructor(private paymentService: PaymentService) {
  }

  test() {
    this.paymentService.testPayment(this.createPaymentLinkRequest).subscribe({
      next: response => {
        window.location.href=''+response.data.checkoutUrl;
      }
    });
  }
}
