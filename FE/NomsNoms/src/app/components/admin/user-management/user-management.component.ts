import {Component, ElementRef, OnInit, Renderer2, ViewChild} from '@angular/core';
import {User} from "../../../_model/user.model";
import {Subject} from "rxjs";
import {UserService} from "../../../_services/user.service";
import {ToastrService} from "ngx-toastr";
import {UserAdmin} from "../../../_model/Admin/userAdmin.model";

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit{
  users: UserAdmin[] = [];
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  };
  dtTrigger: Subject<any> = new Subject<any>();
  @ViewChild('userTable') userTable: ElementRef;
  constructor(private uService: UserService,
              private toastr: ToastrService,
              private renderer: Renderer2) {
  }
  ngOnInit() {
    this.loadUsers();
  }
  loadUsers() {
    this.uService.getUsers().subscribe({
      next: users => {
        this.users = users;
        this.dtTrigger.next(null);
      }
    });
  }

  deleteUser(user: UserAdmin) {
    if (!user) return;
    this.uService.deleteUser(user).subscribe({
      next: _ => {
        if (this.users)
          var user1 = this.users.find(x => x.id == user.id);
        if (user1) {
          user1.status = 0;

          const table = this.userTable.nativeElement;
          const row = table.querySelector(`[data-user-id="${user.id}"]`);

          if (row) {
            const cell = row.cells[3];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', "Vô hiệu hóa");
          }
          this.toastr.success("Vô hiệu hóa người dùng thành công");
        }
      }
    });
  }
  enableUser(user: UserAdmin) {
    console.log(user);
    this.uService.enable(user).subscribe({
      next: _ => {
        if (this.users)
          var user1 = this.users.find(x => x.id == user.id);
        if (user1) {
          user1.status = 1;

          const table = this.userTable.nativeElement;
          const row = table.querySelector(`[data-user-id="${user.id}"]`);

          if (row) {
            const cell = row.cells[3];

            // update the cell content
            this.renderer.setProperty(cell, 'textContent', "Hoạt động");
          }
          this.toastr.success("Mở khóa người dùng thành công");
        }
      }
    });
  }
}
