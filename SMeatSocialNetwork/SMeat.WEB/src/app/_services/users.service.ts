import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { AuthGuard } from '../_guards/auth.guard';

import { User } from '../_models/user';
import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";

//const OPTIONS: RequestOptionsArgs = { headers: new Headers({ 'Content-Type': 'application/json', withCredentials: true }, ) };
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class UsersService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    } 

  getById(id: string): Observable<User> {
    return this.http.get<User>(BASEURL + 'api/users/' + id);
  }

  getMyInfo(): Observable<User>{
    return this.http.get<User>(BASEURL + 'api/users/me');
  }

  update(model): Observable<User> {
    return this.http.put<User>(BASEURL + 'api/users/me', model);
  }

  //delete(id: number) {
  //  return this.http.delete('/api/users/' + id, this.jwt()).map((response: Response) => response.json());
  //}
}
