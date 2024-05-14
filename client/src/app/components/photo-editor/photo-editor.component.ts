import {Component, Input, OnInit, Output} from '@angular/core';
import {FileUploader} from "ng2-file-upload";
import {environment} from "../../../environments/environment";
import {AccountService} from "../../_services/account.service";
import {take} from "rxjs";
import {User} from "../../_model/user.model";

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.scss']
})
export class PhotoEditorComponent implements OnInit{
  @Input() canUploadMultiple = true;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  // artwork: Artwork | undefined;
  user: User | undefined;
  // images: ArtworkImage[] = [];
  // @Output() artworkImagesAdded = new EventEmitter<ArtworkImage>();


  constructor(private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if(user) this.user = user;
      }
    })
  }

  ngOnInit() {
    this.initializeFileUploader();
  }

  fileOverBase(event : any) {
    this.hasBaseDropZoneOver = event;
  }
  initializeFileUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + "image",
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }
    this.uploader.onSuccessItem = (item, response, status, headers) => {

    }
  }
}
