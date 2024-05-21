import {Component, OnInit} from '@angular/core';
import {Subject} from "rxjs";
import {Salary} from "../../../_model/Manager/salary.model";
import {ManagerService} from "../../../_services/manager.service";

@Component({
  selector: 'app-salary-management',
  templateUrl: './salary-management.component.html',
  styleUrls: ['./salary-management.component.css']
})
export class SalaryManagementComponent implements OnInit{
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  salaryList: Salary[] = [];

  constructor(private managerService: ManagerService) {
  }

  ngOnInit() {
    this.managerService.getSalaryList().subscribe({
      next: res => {
        this.salaryList = res;
      }
    });
  }
  exportExcel() {
    var currentDate = new Date();
    this.managerService.exportSalaryist().subscribe(data => {
      this.downloadFile(data, `Danh sách lương của người dùng tháng ${currentDate.getMonth() + 1}.xlsx`);
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
