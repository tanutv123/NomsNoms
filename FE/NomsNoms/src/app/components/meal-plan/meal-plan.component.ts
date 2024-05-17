import {Component, OnInit} from '@angular/core';
import {MealPlan} from "../../_model/mealPlan.model";
import {MealPlanService} from "../../_services/meal-plan.service";
import {PaymentService} from "../../_services/payment.service";
import {CreatePaymentLinkRequest} from "../../_model/createPaymentLinkRequest.model";

@Component({
  selector: 'app-meal-plan',
  templateUrl: './meal-plan.component.html',
  styleUrls: ['./meal-plan.component.scss']
})
export class MealPlanComponent implements OnInit{
  mealPlans: MealPlan[] = [];
  createPaymentLinkRequest: CreatePaymentLinkRequest = {
    returnUrl: "http://localhost:4200/payment-success",
    cancelUrl: "http://localhost:4200/payment-fail"
  } as CreatePaymentLinkRequest;
  constructor(private mpService: MealPlanService,
              private paymentService: PaymentService) {
  }

  ngOnInit() {
    this.loadMealPlans();
  }

  loadMealPlans() {
    this.mpService.getMealPlans().subscribe({
      next: mp => {
        this.mealPlans = mp;
      }
    })
  }
  payment(mealPlanId: number, productName: string, price: number) {
    this.createPaymentLinkRequest.mealPlanId = mealPlanId;
    this.createPaymentLinkRequest.productName = productName;
    this.createPaymentLinkRequest.price = price;
    this.createPaymentLinkRequest.description = "Mua goi mp";
    this.paymentService.testPayment(this.createPaymentLinkRequest).subscribe({
      next: response => {
        window.location.href=''+response.data.checkoutUrl;
      }
    });
  }
}
