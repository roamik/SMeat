import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { Board } from "../_models/board";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";

//const BASEURL = "https://smeat-web-api.herokuapp.com/";
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class BoardsService {
  
  readonly BASEURL: string;

  constructor(private http: HttpClient, private guard: AuthGuard) {
    this.BASEURL = environment.baseApi;
  }
  
  getById(id: string): Observable<Board> {
    return this.http.get<Board>(this.BASEURL + 'api/boards/' + id);
  }

  getBoards(page: number, count: number, searchBy?: string): Observable<Board[]> {
    var url = 'api/boards/paged?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get<Board[]>(this.BASEURL + url);
  }
  getMyBoards(id: string): Observable<Board[]> {
    var url = 'api/boards/my/' + id;
    return this.http.get<Board[]>(this.BASEURL + url);
  }

  likeBoards(id: string): Observable<Board> {
    var url = 'api/boards/like/' + id;
    return this.http.get<Board>(this.BASEURL + url);
  }
  dislikeBoards(id: string): Observable<Board> {
    var url = 'api/boards/dislike/' + id;
    return this.http.get<Board>(this.BASEURL + url);
  }

  add(model): Observable<Board> {
    return this.http.post<Board>(this.BASEURL + 'api/boards', model);
  }
  
}
