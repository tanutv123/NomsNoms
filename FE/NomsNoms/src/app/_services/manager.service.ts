import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Transaction} from "../_model/Manager/transaction.model";
import {Observable} from "rxjs";
import {MealPlanSubscription} from "../_model/Manager/mealPlanSubscription.model";
import {Salary} from "../_model/Manager/salary.model";

@Injectable({
  providedIn: 'root'
})
export class ManagerService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getTransactions() {
    return this.http.get<Transaction[]>(this.baseUrl + 'manager/transactions');
  }
  exportTransactionList(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}manager/export/transaction-list`, { responseType: 'blob' });
  }

  getMealPlanSubscriptions() {
    return this.http.get<MealPlanSubscription[]>(this.baseUrl + 'manager/meal-plan/subscription');
  }
  exportMealPlanSubscriptionist(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}manager/export/mealPlan-subscription-list`, { responseType: 'blob' });
  }
  getSalaryList() {
    return this.http.get<Salary[]>(this.baseUrl + 'manager/users/cook-salary');
  }
  exportSalaryist(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}manager/export/user-salary-list`, { responseType: 'blob' });
  }
}
