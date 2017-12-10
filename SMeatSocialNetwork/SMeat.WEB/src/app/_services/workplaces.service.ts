import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { WorkPlace } from "../_models/workplace";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";

import { environment } from '../../environments/environment';

//const BASEURL = "https://smeat-web-api.herokuapp.com/";
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class WorkplacesService {
    readonly BASEURL: string;
    constructor(private http: Http, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    } 

  getWorkplaces(page: number, count: number, searchBy?: string): Observable<Array<WorkPlace>> {
    var url = 'api/workplaces/paged?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get(this.BASEURL + url, this.guard.jwt())
      .map((response: Response) => response.json());
  }
}
