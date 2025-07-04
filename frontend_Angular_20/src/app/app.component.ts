import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/product';
import { BreadcrumbService } from './core/services/breadcrumb';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({standalone: false,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']

})
export class AppComponent implements OnInit {
  showCenteredOutlet: boolean = false;
  title = 'SkiNet';

  constructor(private basketService: BasketService, private accountService: AccountService,private breadcrumbService: BreadcrumbService) { 


     this.breadcrumbService.hasBreadcrumbs$.subscribe(has => {
    this.showCenteredOutlet = !has;
  });
    
  }

  ngOnInit(): void {
    this.loadBasket();  
    this.loadCurrentUser();
  }

loadCurrentUser() {
  const token = localStorage.getItem('token');
  if (!token) {
    console.warn('No token found in localStorage');
    this.accountService.clearCurrentUser(); // ✅ استخدم الميثود الجديدة
    return;
  }

  this.accountService.loadCurrentUser(token).subscribe({
    next: () => {
      console.log('loaded user');
    },
    error: (error: any) => {
      console.log(error);
      this.accountService.clearCurrentUser(); // ✅ حتى لو حصل error
    }
  });
}




  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log('initialised basket');
      }, error => {
        console.log(error);
      })
    }
  }
}
