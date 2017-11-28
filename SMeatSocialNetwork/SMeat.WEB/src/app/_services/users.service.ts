import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { User } from '../_models/user';
import { Observable } from "rxjs/Observable";

const OPTIONS: RequestOptionsArgs = { headers: new Headers({ 'Content-Type': 'application/json', withCredentials: true }, ) };
const BASEURL = "http://localhost:27121/";

@Injectable()
export class UserService {
  constructor(private http: Http) { }

  getById(id: string) {
    return this.http.get(BASEURL + 'api/users/' + id, this.jwt()).map((response: Response) => response.json());
  }

  getMyInfo(): Observable<User>{
    return this.http.get(BASEURL + 'api/users/me', this.jwt()).map((response: Response) => response.json());
  }

  update(user: User) {
    return this.http.put(BASEURL + 'api/users/' + user.id, user, this.jwt()).map((response: Response) => response.json());
  }


  //delete(id: number) {
  //  return this.http.delete('/api/users/' + id, this.jwt()).map((response: Response) => response.json());
  //}

  // private helper methods

  private jwt() {
    // create authorization header with jwt token
    let currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser && currentUser.token) {
      let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
      return new RequestOptions({ headers: headers });
    }
  }
}
