import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Subject } from 'rxjs/Subject';


@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  public userStateChange: Subject<boolean> = new Subject<boolean>();

  _isAuthenticated: boolean = false;

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (localStorage.getItem('currentUser')) {
      // logged in so return true
      if (!this._isAuthenticated) {
        this.userStateChange.next(true);
      }
      this._isAuthenticated = true;
    }
    else {
      // not logged in so redirect to login page with the return url
      if (this._isAuthenticated) {
        this.userStateChange.next(false);
      }
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      this._isAuthenticated = false;
    }
    return this._isAuthenticated;
  }

  isAuthenticated() {
    return this._isAuthenticated;
  }
}
