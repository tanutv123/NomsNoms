import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-success',
  templateUrl: './success.component.html',
  styleUrls: ['./success.component.css']
})
export class SuccessComponent implements OnInit{
  constructor(private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe({
      next: queryParams => {
        console.log(queryParams['orderCode']);
      }
    });
  }
}
