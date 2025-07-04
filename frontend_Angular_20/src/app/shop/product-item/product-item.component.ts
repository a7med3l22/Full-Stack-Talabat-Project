import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { IProduct } from '../../shared/models/product';

@Component({standalone: false,
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product!: IProduct;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
  }

  addItemToBasket() {
    this.basketService.shipping=0;      //Edit By Me <3 Ahmed Alaa
    this.basketService.addItemToBasket(this.product);
  }

}