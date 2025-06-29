import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from '../../basket/basket.service';
import { IProduct } from '../../shared/models/product';
import { ShopService } from '../shop.service';
import { BreadcrumbService } from '../../core/services/breadcrumb';

@Component({standalone: false,
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product!: IProduct;
  quantity = 1;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService, private basketService: BasketService) {
  }

  ngOnInit(): void {
    this.loadProduct();
  }

 loadProduct() {
  const idParam = this.activatedRoute.snapshot.paramMap.get('id');
  if (!idParam) {
    console.error('No product id in route');
    return;
  }
  const id = +idParam; // حولها لرقم

  this.shopService.getProduct(id).subscribe(product => {
    this.product = product;
    this.bcService.set('@productDetails', product.name);
  }, error => {
    console.log(error);
  });
}


  addItemToBasket() {
    debugger;
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

}
