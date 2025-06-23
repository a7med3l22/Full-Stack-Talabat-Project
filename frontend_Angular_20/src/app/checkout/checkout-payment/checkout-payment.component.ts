import { AfterViewInit, Component, ElementRef, Input, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../../basket/basket.service';
import { IBasket } from '../../shared/models/basket';
import { IOrder } from '../../shared/models/order';
import { CheckoutService } from '../checkout.service';

declare var Stripe: any;

@Component({
  standalone: false,
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {
  @Input() checkoutForm!: FormGroup;
  @ViewChild('cardNumber', { static: true }) cardNumberElement!: ElementRef;
  @ViewChild('cardExpiry', { static: true }) cardExpiryElement!: ElementRef;
  @ViewChild('cardCvc', { static: true }) cardCvcElement!: ElementRef;

  stripe: any;
  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;

  cardErrors: string | null = null;
  loading = false;
  cardNumberValid = false;
  cardExpiryValid = false;
  cardCvcValid = false;

  cardHandler = this.onChange.bind(this);

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngAfterViewInit(): void {
    this.stripe = Stripe('pk_test_51RZSpvQZUUK4xO8RThwZU38ywAKVXS0oWfMW4XrUp9MNlU5jEG9RSlHTFMlZ1y8nkEFxgNocBDweMc4sMLIOyAiY00X7PYryd9');
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardNumber.addEventListener('change', this.cardHandler);

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardExpiry.addEventListener('change', this.cardHandler);

    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCvcElement.nativeElement);
    this.cardCvc.addEventListener('change', this.cardHandler);
  }

  ngOnDestroy(): void {
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }

  onChange(event: any): void {
    if (event.error) {
      this.cardErrors = event.error.message;
    } else {
      this.cardErrors = null;
    }

    switch(event.elementType) {
      case 'cardNumber':
        this.cardNumberValid = event.complete;
        break;
      case 'cardExpiry':
        this.cardExpiryValid = event.complete;
        break;
      case 'cardCvc':
        this.cardCvcValid = event.complete;
        break;
    }
  }

  async submitOrder(): Promise<void> {
    this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();

    try {
      const createdOrder = await this.createOrder(basket!);
      if (!createdOrder) {
        this.toastr.error('فشل إنشاء الطلب');
        this.loading = false;
        return;
      }

   if (basket) {
  const paymentResult = await this.confirmPaymentWithStripe(basket);
  if (paymentResult.paymentIntent) {
    this.basketService.deleteLocalBasket(); // بدون باراميتر لأن الدالة ما تستقبلش
    const navigationExtras: NavigationExtras = { state: createdOrder };
    this.router.navigate(['checkout/success'], navigationExtras);
  } else {
    this.toastr.error(paymentResult.error.message);
  }
} else {
  // ممكن تعالج الحالة لما basket تكون null
  this.toastr.error('Basket is empty or not found.');
}


      this.loading = false;
    } catch (error) {
      console.error(error);
      this.toastr.error('حدث خطأ أثناء إتمام الطلب');
      this.loading = false;
    }
  }

  private async confirmPaymentWithStripe(basket: IBasket): Promise<any> {
    return this.stripe.confirmCardPayment(basket.clientSecret, {
      payment_method: {
        card: this.cardNumber,
        billing_details: {
          name: this.checkoutForm.get('paymentForm')?.get('nameOnCard')?.value
        }
      }
    });
  }

  private async createOrder(basket: IBasket): Promise<IOrder | undefined> {
    const orderToCreate = this.getOrderToCreate(basket);
    return this.checkoutService.createOrder(orderToCreate).toPromise();
  }

  private getOrderToCreate(basket: IBasket) {
    return {
      basketId: basket.id,
      // deliveryMethodId: +this.checkoutForm.get('deliveryForm')?.get('deliveryMethod')?.value, // فعل هذا إذا تستخدم
      shippingAddress: this.checkoutForm.get('addressForm')?.value
    };
  }
}
