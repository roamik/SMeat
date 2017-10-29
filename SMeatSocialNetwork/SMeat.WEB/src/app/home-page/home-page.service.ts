import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

const OPTIONS: RequestOptionsArgs = { headers: new Headers({ 'Content-Type': 'application/json', withCredentials: true }, ) };
const BASEURL = "http://localhost:27121/";

@Injectable()
export class HomePageService {
    private _headers:Headers = new Headers(); 

    
    constructor(private _http: Http) {
        
	  }

    getValues(): Observable<Array<string>> {
        return this._http.get(BASEURL + 'api/values', OPTIONS)
            .map((response: Response) => response.json())
            .catch((error: any) => Observable.throw(error.toString()) || 'GET server error');            
    }
}
