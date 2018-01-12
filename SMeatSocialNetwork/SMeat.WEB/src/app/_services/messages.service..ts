import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';


import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Chat } from '../_models/chat';
import { Message } from '../_models/message';

@Injectable()
export class MessagesService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

  getMessages(page: number, count: number, searchBy: string = '', chatId:string = '', from?:string): Observable<Message[]> {
    var url = `api/messages/paged?page=${page}&count=${count}&from=${new Date(from).toISOString()}&searchBy=${searchBy}&chatId=${chatId}`;
    return this.http.get<Message[]>(this.BASEURL + url);
  }
}