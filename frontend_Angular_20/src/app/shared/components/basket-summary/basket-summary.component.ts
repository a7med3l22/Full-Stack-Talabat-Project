import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IBasketItem } from '../../models/basket';
import { IOrderItem } from '../../models/order';

@Component({
  standalone: false,
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss']
})
export class BasketSummaryComponent implements OnInit {
  @Output() decrement: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() increment: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() remove: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();

  @Input() isBasket: boolean = true; // true = داخل سلة المشتريات، false = للعرض فقط
  @Input() items: IBasketItem[] | IOrderItem[] = [];
  @Input() isOrder: boolean = false;

  constructor() { }

  ngOnInit(): void {
    // يمكن تتبع البيانات هنا عند الحاجة
    // console.log(this.items);
  }
get typedItems(): IBasketItem[] {
  return this.items as IBasketItem[];
}

  decrementItemQuantity(item: IBasketItem) {
    this.decrement.emit(item);
  }

  incrementItemQuantity(item: IBasketItem) {
    this.increment.emit(item);
  }

  removeBasketItem(item: IBasketItem) {
    this.remove.emit(item);
  }
}
