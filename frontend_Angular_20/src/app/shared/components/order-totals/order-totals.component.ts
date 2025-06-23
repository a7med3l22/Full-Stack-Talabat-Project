import { Component, Input, OnInit } from '@angular/core';

@Component({standalone: false,
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
@Input() shippingPrice: number = 0;
@Input() subtotal: number = 0;
@Input() total: number = 0;
@Input() OrderState: string = '';


  constructor() { }

  ngOnInit(): void {
  }

}