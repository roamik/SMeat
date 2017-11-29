import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { AuthGuard } from '../_guards/auth.guard';

import { User } from '../_models/user';
import { Observable } from "rxjs/Observable";

const OPTIONS: RequestOptionsArgs = { headers: new Headers({ 'Content-Type': 'application/json', withCredentials: true }, ) };
const BASEURL = "http://localhost:27121/";

@Injectable()
export class UserService {
  constructor(private http: Http, private guard: AuthGuard ) { }

  getById(id: string) {
    return this.http.get(BASEURL + 'api/users/' + id, this.guard.jwt()).map((response: Response) => response.json());
  }

  getMyInfo(): Observable<User>{
    return this.http.get(BASEURL + 'api/users/me', this.guard.jwt()).map((response: Response) => response.json());
  }

  update(model) {
    return this.http.put(BASEURL + 'api/users/me', model, this.guard.jwt()).map((response: Response) => response.json());
  }

  //delete(id: number) {
  //  return this.http.delete('/api/users/' + id, this.jwt()).map((response: Response) => response.json());
  //}
}
