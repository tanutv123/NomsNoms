import {Component, OnInit} from '@angular/core';
import {MealPlan} from "../../../../_model/mealPlan.model";
import {RecipeAdminService} from "../../../../_services/recipe-admin.service";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

@Component({
  selector: 'app-mealplan-create',
  templateUrl: './mealplan-create.component.html',
  styleUrls: ['./mealplan-create.component.css']
})
export class MealplanCreateComponent implements OnInit{
  validationErrors: string[] = [];
  mp: MealPlan = {} as MealPlan;

  constructor(private adminService: RecipeAdminService,
              private toastr: ToastrService,
              private router: Router) {
  }

  ngOnInit() {
  }

  createMealPlan() {
    this.adminService.createMealPlan(this.mp).subscribe({
      next: _ => {
        this.toastr.success('Tạo thành công');
        this.router.navigateByUrl('/admin-mp');
      }
    });
  }
}
