import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from '../../shared/models/order';
import { OrdersService } from '../orders.service';
import { BreadcrumbService } from '../../core/services/breadcrumb';

@Component({standalone:false,
  selector: 'app-order-detailed',
  templateUrl: './order-detailed.component.html',
  styleUrls: ['./order-detailed.component.scss']
})
export class OrderDetailedComponent implements OnInit {
  order!: IOrder;

  constructor(
    private route: ActivatedRoute,
    private breadcrumbService: BreadcrumbService,
    private orderService: OrdersService
  ) {}

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (!idParam) {
      console.error('ID parameter is missing');
      return;
    }

    const id = +idParam;

    this.orderService.getOrderDetailed(id).subscribe({
      next: (order: IOrder) => {
        if (!order) return;

        this.order = order;

        // هنا بس نغير اسم الـ breadcrumb لما يكون في فعلاً order
        this.breadcrumbService.set('@OrderDetailed', `Order #${order.id} - ${order.orderState}`);
      },
      error: error => {
        console.error(error);
      }
    });
  }
}
