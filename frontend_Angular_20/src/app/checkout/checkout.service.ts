import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { IOrder, IOrderToCreate } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }
createOrder(order: IOrderToCreate) {
  return this.http.post<IOrder>(this.baseUrl + 'order', order);
}

getDeliveryMethods() {
  return this.http.get<IDeliveryMethod[]>(this.baseUrl + 'order/deliveryMethods').pipe(
    map(dm => {
      return dm.sort((a, b) => b.cost - a.cost);
    })
  );
}
}
