import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';
@Component({standalone: false,
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  errors!: string[];

  constructor(private fb: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }
/*
{
  "displayName": "string",
  "email": "user@example.com",
  "password": "string",
  "confirmPassword": "string",
  "phoneNumber": "string",
  "address": {
    "firstName": "string",
    "lastName": "string",
    "street": "string",
    "city": "string",
    "country": "string"
  }
}
*/
createRegisterForm() {
  this.registerForm = this.fb.group({
    displayName: [null, [Validators.required]],
    email: [null,
      [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
      [this.validateEmailNotTaken()]
    ],
    phoneNumber: [null, [Validators.required, Validators.pattern('^01[0-2,5]{1}[0-9]{8}$')]],
    password: [null, [Validators.required]],
    confirmPassword: [null, [Validators.required]],

    // ✅ إضافة عنوان داخلي كـ FormGroup
    address: this.fb.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      street: [null, Validators.required],
      city: [null, Validators.required],
      country: [null, Validators.required]
    })
  }, { validator: this.passwordsMatchValidator });
}

passwordsMatchValidator(form: FormGroup) {
  const password = form.get('password')?.value;
  const confirmPassword = form.get('confirmPassword')?.value;
  return password === confirmPassword ? null : { mismatch: true };
}


  onSubmit() {
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/shop');
    }, error => {
      console.log(error);
      this.errors = error.errors;
    })
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map(res => {
               return res ? {emailExists: true} : null;
            })
          );
        })
      )
    }
  }

}
