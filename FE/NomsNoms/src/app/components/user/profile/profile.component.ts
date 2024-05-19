import {Component, OnInit} from '@angular/core';
import {User} from "../../../_model/user.model";
import {ActivatedRoute} from "@angular/router";
import {Recipe} from "../../../_model/recipe.model";
import {RecipeService} from "../../../_services/recipe.service";
import {AccountService} from "../../../_services/account.service";
import {Subject} from "rxjs";
import {SubscriptionModel} from "../../../_model/subscription.model";
import {UserService} from "../../../_services/user.service";
import {PaymentService} from "../../../_services/payment.service";
import {CreateSubscriptionPaymentLinkRequest} from "../../../_model/createSubscriptionPaymentLinkRequest.model";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit{
  openTab = 1;
  user: User | undefined;
  recipes: Recipe[] = [];
  likedRecipes: Recipe[] = [];
  followers: User[] = [];
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  subscription: SubscriptionModel | undefined;
  constructor(private route: ActivatedRoute,
              private recipeService: RecipeService,
              private userService: UserService,
              private paymentService: PaymentService,
              private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => {
        this.user = data['user'];
        this.loadRecipes();
        this.loadFollowers();
        this.loadLikedRecipes();
        this.loadSubscription();
      }
    });
  }
  loadSubscription() {
    if (!this.user) return;
    this.userService.getUserSubscription(this.user.email).subscribe({
      next: res => {
        this.subscription = res;
      }
    });
  }
  subscribeUser() {
    if (!this.subscription || !this.user) return;
    var request: CreateSubscriptionPaymentLinkRequest = {
      subscriptionId: this.subscription.subscriptionId,
      returnUrl: "http://localhost:4200/payment-success",
      cancelUrl: "http://localhost:4200/payment-fail",
      price: this.subscription.price,
      productName: 'Gói hội viên của ' + this.user.email,
      description: 'subscription'
    };
    this.paymentService.subcribePayment(request).subscribe({
      next: res => {
        window.location.href=''+res.data.checkoutUrl;
      }
    })
  }
  loadLikedRecipes() {
    this.recipeService.getLikedRecipe().subscribe({
      next: recipes => {
        this.likedRecipes = recipes;
        this.dtTrigger.next(null);
      }
    });
  }
  loadRecipes() {
    if (!this.user) return;
    this.recipeService.getUserProfileRecipe(this.user.email).subscribe({
      next: recipes => {
          this.recipes = recipes;
          console.log(this.recipes);
      }
    });
  }
  loadFollowers() {
    if (!this.user) return;
    this.accountService.getFollowers(this.user.email).subscribe({
      next: followers => {
        this.followers = followers;
      }
    });
  }
  toggleTabs($tabNumber: number){
    this.openTab = $tabNumber;

  }
}
