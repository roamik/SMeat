import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { HttpClient, HttpHeaders } from "@angular/common/http";

const OPTIONS: RequestOptionsArgs = { headers: new Headers({ 'Content-Type': 'application/json', withCredentials: true }, ) };
//const HEADERS: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json', 'withCredentials': 'true' });
const BASEURL = "http://localhost:27121/";


@Injectable()
export class AuthenticationService {
  constructor(private http: Http){ }

  public register(model) {
    return this.http.post(BASEURL + 'api/account/register', model, OPTIONS)
          .map((response: Response) => {
              // login successful if there's a jwt token in the response
              let user = response.json();
              if (user && user.token) {
                  // store user details and jwt token in local storage to keep user logged in between page refreshes
                  localStorage.setItem('currentUser', JSON.stringify(user));
              }

              return user;
          });
  }

  public login(model) {
    return this.http.post(BASEURL + 'api/account/login', model, OPTIONS)
      .map((response: Response) => {
        // login successful if there's a jwt token in the response
        let user = response.json();
        if (user && user.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
        }

        return user;
      });
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
  }

  public getToken(): string {
    return localStorage.getItem('token');
  }
}
