import {Component, OnInit} from '@angular/core';
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";
import {ActivatedRoute} from "@angular/router";
import {ToastrService} from "ngx-toastr";
import {MealPlan} from "../../../../_model/mealPlan.model";

@Component({
  selector: 'app-mealplan-detail',
  templateUrl: './mealplan-detail.component.html',
  styleUrls: ['./mealplan-detail.component.css']
})
export class MealplanDetailComponent implements OnInit{
  mp: MealPlan | undefined;
  validationErrors: string[] = [];
  constructor(private adminService: RecipeAdminService,
              private route: ActivatedRoute,
              private toastr: ToastrService) {
  }

  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.mp = data['mealPlan'];
      }
    });
  }

  updateMealPlan() {
    if (!this.mp) return;
    this.adminService.updateMealPlan(this.mp).subscribe({
      next: _ => {
        this.toastr.success('Cập nhật thành công');
      }
    });
  }

}
