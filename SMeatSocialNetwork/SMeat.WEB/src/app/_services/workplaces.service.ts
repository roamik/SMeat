import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { WorkPlace } from "../_models/workplace";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { HttpClient } from "@angular/common/http";


const BASEURL = "http://localhost:27121/";

@Injectable()
export class WorkplacesService {
  constructor(private http: HttpClient, private guard: AuthGuard) { }

  getWorkplaces(page: number, count: number, searchBy?: string): Observable<WorkPlace[]> {
    var url = 'api/workplaces/paged?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get<WorkPlace[]>(BASEURL + url);
  }
}
