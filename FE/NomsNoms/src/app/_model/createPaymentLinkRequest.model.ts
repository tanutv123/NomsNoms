export interface CreatePaymentLinkRequest {
  productName: string;
  mealPlanId: number;
  description: string;
  price: number;
  returnUrl: string;
  cancelUrl: string;
}
