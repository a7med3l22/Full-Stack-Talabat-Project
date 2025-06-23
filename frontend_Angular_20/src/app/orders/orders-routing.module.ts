import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrdersComponent } from './orders.component';
import { OrderDetailedComponent } from './order-detailed/order-detailed.component';

const routes: Routes = [
  {
    path: '',
    component: OrdersComponent,
    data: { breadcrumb: 'Orders' }
  },
  {
    path: ':id',
    component: OrderDetailedComponent,
    data: { breadcrumb: '' } // ده اسم ديناميكي هيتغير من BreadcrumbService
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class OrdersRoutingModule { }
