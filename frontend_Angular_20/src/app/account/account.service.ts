import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { of, ReplaySubject, Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { IAddress } from '../shared/models/address';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
private currentUserSource = new ReplaySubject<IUser | null>(1);
public currentUser$ = this.currentUserSource.asObservable();


  constructor(private http: HttpClient, private router: Router) { }

// داخل AccountService
clearCurrentUser() {
  this.currentUserSource.next(null);
}


  loadCurrentUser(token: string | null): Observable<IUser | null> {
    if (!token) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<IUser>(this.baseUrl + 'account', { headers }).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        } else {
          localStorage.removeItem('token');
          this.currentUserSource.next(null);
          return null;
        }
      }),
  catchError(error => {
    // في حالة أي خطأ (زي 401 أو 500) نمسح التوكن بردو
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    return of(null);
  })
    );
  }

  login(values: any): Observable<IUser | null> {
    return this.http.post<IUser>(this.baseUrl + 'account/login', values).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        }
        return null;
      })
    );
  }

  register(values: any): Observable<IUser | null> {
    return this.http.post<IUser>(this.baseUrl + 'account/register', values).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        }
        return null;
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + 'account/emailexists?email=' + email);
  }

  getUserAddress(): Observable<IAddress> {
    return this.http.get<IAddress>(this.baseUrl + 'account/GetUserAddress');
  }

  updateUserAddress(address: IAddress): Observable<IAddress> {
    return this.http.put<IAddress>(this.baseUrl + 'account/UpdateUserAddress', address);
  }
}
