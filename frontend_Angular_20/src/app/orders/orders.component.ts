import { Component, OnInit } from '@angular/core';
import { IOrder } from '../shared/models/order';
import { OrdersService } from './orders.service';

@Component({standalone: false,
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
orders: IOrder[] = []; // أفضل

  constructor(private orderService: OrdersService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders(): void {
  this.orderService.getOrdersForUser().subscribe({
    next: (orders: IOrder[]) => {
      this.orders = orders;
    },
    error: (error) => {
      console.error('حدث خطأ أثناء جلب الطلبات:', error);
    }
  });
}


}
