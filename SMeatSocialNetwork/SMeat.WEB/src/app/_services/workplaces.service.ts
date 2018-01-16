import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { WorkPlace } from "../_models/workplace";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { HttpClient } from "@angular/common/http";

import { environment } from '../../environments/environment';

//const BASEURL = "https://smeat-web-api.herokuapp.com/";
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class WorkplacesService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    } 

  getWorkplaces(page: number, count: number, searchBy?: string): Observable<WorkPlace[]> {
    var url = 'api/workplaces/paged?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get<WorkPlace[]>(this.BASEURL + url);
    }

  add(model): Observable<WorkPlace> {
    return this.http.post<WorkPlace>(this.BASEURL + 'api/workplaces', model);
  }
}
