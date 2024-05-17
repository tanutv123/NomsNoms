import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {CreatePaymentLinkRequest} from "../_model/createPaymentLinkRequest.model";

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  testPayment(createPaymentLinkRequest: CreatePaymentLinkRequest) {
    return this.http.post<any>(this.baseUrl + 'order/create', createPaymentLinkRequest);
  }
  verifyPayment(orderCode: number) {
    return this.http.get<any>(this.baseUrl + 'order/' + orderCode);
  }
}
