import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { Headers, RequestOptions, RequestOptionsArgs } from '@angular/http';


@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  private get currentUser(): any { return localStorage.getItem('currentUser') !== null ? JSON.parse(localStorage.getItem('currentUser')) : null }

  public get userId(): string { return this.currentUser !== null ? this.currentUser.id : null }

  public get token(): string { return this.currentUser !== null ? this.currentUser.token : null }

  get isAuthenticated(): boolean { return this.currentUser !== null };

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.isAuthenticated) {
      // logged in so return true
    }
    else {
      // not logged in so redirect to login page with the return url
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    }
    return this.isAuthenticated;
  }

  public jwt() {
    // create authorization header with jwt token
    if (this.token) {
      let headers = new Headers({ 'Authorization': 'Bearer ' + this.token });
      return new RequestOptions({ headers: headers });
    }
  }
}
