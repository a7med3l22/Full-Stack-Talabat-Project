import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({standalone: false,
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  @Input() totalCount!: number;
  @Input() pageSize!: number;
  @Input() pageNumber!: number;
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

onPagerChange(page: number) {
  this.pageChanged.emit(page);
}


}
