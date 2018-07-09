import { Injectable } from '@angular/core';

import { AuthGuard } from '../_guards/auth.guard';

import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs/Observable';
import { User } from '../_models/user';

@Injectable()
export class UploadService {

  readonly BASEURL: string;

  constructor(private http: HttpClient, private guard: AuthGuard) {
    this.BASEURL = environment.baseApi;
  }


  uploadUserImage(fileToUpload: any) {
    let input = new FormData();
    input.append("file", fileToUpload);

    return this.http.post<User>(this.BASEURL + 'api/upload/avatar/', input);
  }

  uploadImage(fileToUpload: any) {
    let input = new FormData();
    input.append("file", fileToUpload);

    return this.http.post<{ path: string }>(this.BASEURL + 'api/upload/file/', input);
  }
}
