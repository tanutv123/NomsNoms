import {Component, OnInit} from '@angular/core';
import {User} from "../../../_model/user.model";
import {Recipe} from "../../../_model/recipe.model";
import {ActivatedRoute} from "@angular/router";
import {RecipeService} from "../../../_services/recipe.service";
import {AccountService} from "../../../_services/account.service";
import {ToastrService} from "ngx-toastr";
import {Image} from "../../../_model/image.model";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit{
  user: User | undefined;
  validationErrors: string[] = [];
  image :Image | undefined;
  constructor(private route: ActivatedRoute,
              private accountService: AccountService,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => {
        this.user = data['user'];
        console.log(this.user);
      }
    });
  }

  updateAvatar() {
    if (!this.image) return;
    this.accountService.updateAvatar(this.image).subscribe({
      next: _ => {
        this.toastr.success("Cập nhật ảnh đại diện thành công");
      }
    });
  }

  onImageAdded(imageData: Image) {
    this.image = imageData;
    this.toastr.success("Tải ảnh thành công");
  }

  updateUser() {
    if (!this.user) return;
    this.accountService.updateUserProfile(this.user).subscribe({
      next: _ => {
        this.toastr.success("Cập nhật thành công");
      },
      error: err => {
        this.validationErrors = err;
      }
    });
  }
}
