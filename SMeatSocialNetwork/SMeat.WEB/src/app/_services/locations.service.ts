import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { Location } from "../_models/location";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";

//const BASEURL = "https://smeat-web-api.herokuapp.com/";
//const BASEURL = "http://localhost:27121/";

@Injectable()
export class LocationsService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

  getLocations(page: number, count: number, searchBy?: string): Observable<Location[]> {
    var url = 'api/locations/paged?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy : '');
    return this.http.get<Location[]>(BASEURL + url);
  }
}
