import { Component, Input, OnInit } from '@angular/core';

@Component({standalone: false,
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent implements OnInit {
  @Input() pageNumber: number = 1;
@Input() pageSize: number = 6;
@Input() totalCount: number = 0;


  constructor() { }

  ngOnInit(): void {
  }

}
