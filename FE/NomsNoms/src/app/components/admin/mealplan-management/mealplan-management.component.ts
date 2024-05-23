import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {MealPlanSubscription} from "../../../_model/Manager/mealPlanSubscription.model";
import {Subject} from "rxjs";
import {RecipeAdminService} from "../../../_services/recipe-admin.service";
import {MealPlan} from "../../../_model/mealPlan.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-mealplan-management',
  templateUrl: './mealplan-management.component.html',
  styleUrls: ['./mealplan-management.component.css']
})
export class MealplanManagementComponent implements OnInit{
  mealPlans: MealPlan[]= [];
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  @ViewChild('mealPlanTable') mealPlanTable: ElementRef;

  constructor(private adminService: RecipeAdminService,
              private toastr: ToastrService,
              private renderer: Renderer2) {
  }

  ngOnInit() {
    this.loadMealPlanSubscriptions();
  }

  loadMealPlanSubscriptions() {
    this.adminService.getMealPlans().subscribe({
      next: res => {
        this.mealPlans = res;
      }
    });
  }

  enableMp(mp: MealPlan){
    this.adminService.enableMealPlan(mp).subscribe({
      next: _ => {
        if (this.mealPlans)
          var user1 = this.mealPlans.find(x => x.id == mp.id);
        if (user1) {
          user1.status = 1;

          const table = this.mealPlanTable.nativeElement;
          const row = table.querySelector(`[data-mp-id="${mp.id}"]`);

          if (row) {
            const cell = row.cells[5];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', "Hoạt động");
          }
          this.toastr.success("Mở thành công");
        }
      }
    });
  }
  disableMp(mp: MealPlan){
    this.adminService.deleteMealPlan(mp).subscribe({
      next: _ => {
        if (this.mealPlans)
          var user1 = this.mealPlans.find(x => x.id == mp.id);
        if (user1) {
          user1.status = 0;

          const table = this.mealPlanTable.nativeElement;
          const row = table.querySelector(`[data-mp-id="${mp.id}"]`);

          if (row) {
            const cell = row.cells[5];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', "Vô hiệu hóa");
          }
          this.toastr.success("Vô hiệu hóa thành công");
        }
      }
    });
  }
}
