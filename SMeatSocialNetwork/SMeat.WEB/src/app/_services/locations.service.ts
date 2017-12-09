import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';

import { Location } from "../_models/location";

import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";


const BASEURL = "https://smeat-web-api.herokuapp.com/";

@Injectable()
export class LocationsService {
  constructor(private http: Http, private guard: AuthGuard) { }

  getLocations(page: number, count: number, searchBy?: string): Observable<Array<Location>> {
    var url = 'api/locations/paged?page=' + page + '&count=' + count + (searchBy ? '&searchBy=' + searchBy: '');
    return this.http.get(BASEURL + url, this.guard.jwt())
      .map((response: Response) => response.json());
  }
}
