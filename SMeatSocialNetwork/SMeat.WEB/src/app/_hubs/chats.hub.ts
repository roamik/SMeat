import { Inject, Injectable } from "@angular/core";
import { BaseHub } from "../_hubs/base.hub";
import { Observable } from "rxjs/Observable";
import { User } from "../_models/user";
import { AuthGuard } from "../_guards/auth.guard";
import { Message } from '../_models/message';

@Injectable()
export class ChatHub extends BaseHub {

  constructor(protected guard: AuthGuard) {
    super("chat", guard);
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

  sendMessage(message:Message): Observable<Message> {
    return this.hubConnection.invoke("SendAsync", message);    
  }

  connectToChat(chatId): Observable<any> {
    return this.hubConnection.invoke("ConnectToChatAsync", chatId);    
  }

  started(): Observable<any> {
    return this.starting;
  }

}
