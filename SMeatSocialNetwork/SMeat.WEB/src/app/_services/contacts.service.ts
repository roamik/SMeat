
import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { AuthGuard } from '../_guards/auth.guard';

import { User } from '../_models/user';
import { Request } from '../_models/request';
import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Friend } from '../_models/friend';

//const OPTIONS: RequestOptionsArgs = { headers: new Headers({ 'Content-Type': 'application/json', withCredentials: true }, ) };
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class ContactsService {
  readonly BASEURL: string;
  constructor(private http: HttpClient, private guard: AuthGuard) {
    this.BASEURL = environment.baseApi;
  }

  addContact(id: string): Observable<User> {
    return this.http.post<User>(this.BASEURL + 'api/contacts/add/' + id, null);
  }

  removeContact(id: string): Observable<User> {
    return this.http.post<User>(this.BASEURL + 'api/contacts/remove/' + id, null);
  }

  getRequests(page: number, count: number, searchBy?: string): Observable<Request[]> {
    var url = 'api/contacts/requests?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get<Request[]>(this.BASEURL + url);
  }

  getContacts(page: number, count: number, searchBy?: string): Observable<Friend[]> {
    var url = 'api/contacts/contacts?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get<Friend[]>(this.BASEURL + url);
  }
}
