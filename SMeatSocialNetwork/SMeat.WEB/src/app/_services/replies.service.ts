import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { Reply } from "../_models/reply";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";

//const BASEURL = "https://smeat-web-api.herokuapp.com/";
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class RepliesService {
  
  readonly BASEURL: string;

  constructor(private http: HttpClient, private guard: AuthGuard) {
    this.BASEURL = environment.baseApi;
  }

  getByBoardId(id: string): Observable<Reply[]> {
    var url = 'api/replies/' + id;
    return this.http.get<Reply[]>(this.BASEURL + url);
  }

  getCountByBoardId(id: string): Observable<number> {
    var url = 'api/replies/count/' + id;
    return this.http.get<number>(this.BASEURL + url);
  }
  
  add(model): Observable<Reply> {
    return this.http.post<Reply>(this.BASEURL + 'api/replies', model);
  }
  
}
