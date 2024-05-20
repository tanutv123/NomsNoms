import {Component, Inject} from '@angular/core';
import {
  MAT_DIALOG_DATA,
  MatDialogRef,
} from "@angular/material/dialog";

@Component({
  selector: 'app-taste-profile',
  templateUrl: './taste-profile.component.html',
  styleUrls: ['./taste-profile.component.css']
})
export class TasteProfileComponent {
  constructor(
    public dialogRef: MatDialogRef<TasteProfileComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
