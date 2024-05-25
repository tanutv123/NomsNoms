import {Component, OnInit} from '@angular/core';
import {Subject} from "rxjs";
import {MealPlanSubscription} from "../../../_model/Manager/mealPlanSubscription.model";
import {ManagerService} from "../../../_services/manager.service";

@Component({
  selector: 'app-meal-plans',
  templateUrl: './meal-plans.component.html',
  styleUrls: ['./meal-plans.component.css']
})
export class MealPlansComponent implements OnInit{
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  mealPlans: MealPlanSubscription[] = [];
  constructor(private managerService: ManagerService) {
  }

  ngOnInit() {
    this.managerService.getMealPlanSubscriptions().subscribe({
      next: res => {
        this.mealPlans = res;
        this.dtTrigger.next(null);
      }
    });
  }
  exportExcel() {
    this.managerService.exportMealPlanSubscriptionist().subscribe(data => {
      this.downloadFile(data, `Danh sách người dùng đăng ký meal plan.xlsx`);
    }, error => {
      console.error('Error exporting excel:', error);
    });
  }
  private downloadFile(data: Blob, filename: string) {
    const url = window.URL.createObjectURL(data);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = filename;
    anchor.click();
    window.URL.revokeObjectURL(url);
  }
}
