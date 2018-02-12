import { Inject, Injectable } from "@angular/core";
import { BaseHub } from "../_hubs/base.hub";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/fromPromise';
import { User } from "../_models/user";
import { AuthGuard } from "../_guards/auth.guard";
import { Message } from '../_models/message';
import { UserStatusType } from "../_enums/user-status";

@Injectable()  

export class ChatHub extends BaseHub {

  constructor(protected guard: AuthGuard) {
    super("chat", guard);
  }
  
  onNewUserAdded(fn:(connectionId: string, user: User, error: string) => void) {
    this.hubConnection.on("onNewUserAdded", fn);
  }

  onDisconnected(fn:(connectionId: string, user: User, error: string) => void) {
    this.hubConnection.on("OnDisconnected", fn);
  }

  onConnected( fn:(connectionId: string, user: User, message: Message) => void) {
    this.hubConnection.on("OnConnected", fn);
  }

  onSend( fn:(connectionId: string, user: User, message: Message) => void) {
    this.hubConnection.on("OnSend", fn);
  }

  onConnectToChat(fn:(connectionId: string, user: User, message: Message) => void) {
    this.hubConnection.on("OnConnectToChat", fn);
  }

  onUserStatusChange(fn:(connectionId: string, userId: string, status: UserStatusType) => void) {
    this.hubConnection.on("OnUserStatusChange", fn);
  }

  sendMessage(message:Message): Observable<Message> {
    return Observable.fromPromise(this.hubConnection.invoke("SendAsync", message));    
  }

  connectToChat(chatId): Observable<any> {
    return Observable.fromPromise(this.hubConnection.invoke("ConnectToChatAsync", chatId));    
  }

  changeUserStatus(status, chatId) {
    return Observable.fromPromise(this.hubConnection.invoke("UserStatusChangeAsync", status, chatId));
  }

  started(): Observable<any> {
    return this.starting;
  }

  closed(): Observable<any> {
    return this.closing;
  }
}
