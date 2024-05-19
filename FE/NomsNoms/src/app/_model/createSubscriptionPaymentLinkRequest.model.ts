export interface CreateSubscriptionPaymentLinkRequest{
  productName: string;
  subscriptionId: number;
  description: string;
  price: number;
  returnUrl: string;
  cancelUrl: string;
}
