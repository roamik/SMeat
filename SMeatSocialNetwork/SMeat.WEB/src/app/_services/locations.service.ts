import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { Location } from "../_models/location";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { HttpClient } from "@angular/common/http";


const BASEURL = "http://localhost:27121/";

@Injectable()
export class LocationsService {
  constructor(private http: HttpClient, private guard: AuthGuard) { }

  getLocations(page: number, count: number, searchBy?: string): Observable<Location[]> {
    var url = 'api/locations/paged?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get<Location[]>(BASEURL + url);
  }
}
