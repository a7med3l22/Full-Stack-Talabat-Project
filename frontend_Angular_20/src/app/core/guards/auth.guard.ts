import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../../account/account.service';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  
canActivate(
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
): Observable<boolean> {
  console.log('✅ AuthGuard تم استدعاؤه للمسار:', state.url); // <-- هنا

  return this.accountService.currentUser$.pipe(
    map(auth => {
      console.log('✅ الحالة الحالية للمستخدم:', auth); // <-- وهنا كمان

      if (auth) {
        return true;
      }

      this.router.navigate(['account/login'], {
        queryParams: { returnUrl: state.url }
      });
      return false;
    })
  );
}

}
