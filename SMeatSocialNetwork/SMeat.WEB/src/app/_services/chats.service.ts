import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, RequestOptionsArgs, Response } from '@angular/http';


import { AuthGuard } from '../_guards/auth.guard';

import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Chat } from '../_models/chat';
import { Message } from '../_models/message';

@Injectable()
export class ChatsService {
    readonly BASEURL: string;
    constructor(private http: HttpClient, private guard: AuthGuard) {
        this.BASEURL = environment.baseApi;
    }

  getChats(page: number, count: number, searchBy: string = ''): Observable<Chat[]> {
    var url = `api/chats/paged?page=${page}&count=${count}&searchBy=${searchBy}`;
    return this.http.get<Chat[]>(this.BASEURL + url);
  }

  getChatMessages(chatId:string, page: number, count: number, searchBy: string = ''): Observable<Message[]> {
    var url = `api/chat/${chatId}/paged?page=${page}&count=${count}&searchBy=${searchBy}`;
    return this.http.get<Message[]>(this.BASEURL + url);
  }

  add(model): Observable<Chat> {
    return this.http.post<Chat>(this.BASEURL + 'api/chats', model);
  }
}
