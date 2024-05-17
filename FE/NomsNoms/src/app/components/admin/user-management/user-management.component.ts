import {Component, OnInit} from '@angular/core';
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
  constructor(private uService: UserService,
              private toastr: ToastrService) {
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
    console.log(user);
    this.uService.deleteUser(user).subscribe({
      next: _ => {
        this.loadUsers();
        this.toastr.success("Vô hiệu hóa người dùng thành công");
      }
    });
  }
}
