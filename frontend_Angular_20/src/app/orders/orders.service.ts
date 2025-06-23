import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { IOrder } from '../shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
 baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

 getOrdersForUser(): Observable<IOrder[]> {
  return this.http.get<IOrder[]>(this.baseUrl + 'order');
}

getOrderDetailed(id: number): Observable<IOrder> {
  return this.http.get<IOrder>(`${this.baseUrl}order/${id}`);
}

}