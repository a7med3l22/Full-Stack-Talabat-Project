import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../account/account.service';
import { IAddress } from '../../shared/models/address';

@Component({standalone:false,
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
  
  @Input({ required: true }) checkoutForm!: FormGroup;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  saveUserAddress(): void {
    const addressForm = this.checkoutForm.get('addressForm');
    if (!addressForm) {
      this.toastr.error('Form غير صالح');
      return;
    }

    this.accountService.updateUserAddress(addressForm.value).subscribe({
      next: (address: IAddress) => {
        this.toastr.success('تم حفظ العنوان بنجاح');
        addressForm.reset(address);
      },
      error: (error) => {
        this.toastr.error(error?.message || 'فشل في حفظ العنوان');
        console.error(error);
      }
    });
  }
}
