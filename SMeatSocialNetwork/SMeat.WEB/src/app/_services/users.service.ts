import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { AuthGuard } from '../_guards/auth.guard';

import { User } from '../_models/user';
import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";

const OPTIONS: RequestOptionsArgs = { headers: new Headers({ 'Content-Type': 'application/json', withCredentials: true }, ) };
//const BASEURL = "https://smeat-web-api.herokuapp.com/";
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class UsersService {
    readonly BASEURL: string;
    constructor(private http: Http, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    } 

  getById(id: string) {
    return this.http.get(this.BASEURL + 'api/users/' + id, this.guard.jwt()).map((response: Response) => response.json());
  }

  getMyInfo(): Observable<User>{
      return this.http.get(this.BASEURL + 'api/users/me', this.guard.jwt()).map((response: Response) => response.json());
  }

  update(model) {
      return this.http.put(this.BASEURL + 'api/users/me', model, this.guard.jwt()).map((response: Response) => response.json());
  }

  //delete(id: number) {
  //  return this.http.delete('/api/users/' + id, this.jwt()).map((response: Response) => response.json());
  //}
}
