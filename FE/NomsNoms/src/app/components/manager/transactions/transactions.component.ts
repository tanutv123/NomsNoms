import {Component, OnInit} from '@angular/core';
import {Subject} from "rxjs";
import {Transaction} from "../../../_model/Manager/transaction.model";
import {ManagerService} from "../../../_services/manager.service";

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit{
  transactions: Transaction[] = [];
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  constructor(private managerService: ManagerService) {
  }

  ngOnInit() {
    this.managerService.getTransactions().subscribe({
      next: res => {
        this.transactions = res;
        this.dtTrigger.next(null);
      }
    })
  }

  exportTransactionList() {
    var currentDate = new Date();
    this.managerService.exportTransactionList().subscribe(data => {
      this.downloadFile(data, `Giao dịch NomsNoms tháng ${currentDate.getMonth() + 1}.xlsx`);
    }, error => {
      console.error('Error exporting transaction list:', error);
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
