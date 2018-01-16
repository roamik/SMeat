import { Component, OnInit, Input } from '@angular/core';
import { Chat } from '../_models/chat';
import { ChatsService } from '../_services/chats.service';
import { BaseTosterService } from '../_services/base-toaster.service';
import { Message } from '../_models/message';

import * as _ from "lodash";
import { ChatHub } from '../_hubs/chats.hub';
import { AuthGuard } from '../_guards/auth.guard';
import { User } from '../_models/user';

@Component({
  selector: 'chat-preview',
  providers: [ ChatHub ],
  templateUrl: './chat-preview.component.html',
  styleUrls: ["../chat-list-page/chat-list-page.component.css"]
})
export class ChatPreviewComponent implements OnInit {
  
  unreadCount: number = 0;
  _isLoaded: boolean;

  @Input('isSelected')
  set isSelected(value:boolean) {
    this._isLoaded = value;
    this.unreadCount = 0;
  }
  get isSelected():boolean {
    return this._isLoaded;
  }

  @Input() chat: Chat;
  @Input() user: User;

  constructor(private chatsService: ChatsService, private tosterService: BaseTosterService, private chatHub: ChatHub) {  }

  get preview(): Message {
    return _.last(this.chat.messages);
  };
  

  ngOnInit() {
    this.chatHub.started().subscribe(
      sucsses => {
        this.onHubConnected();
        this.tosterService.info("HUB", "started")
      },
      error => { this.tosterService.error("HUB", "not started") }
    )
    this.chatHub.start();
  }

  onHubConnected() {
    this.chatHub.onSend((connectionId: string, user: User, message: Message) => {
        this.tosterService.info("HUB", "send");
        var lastMessageIndex = _.findIndex(this.chat.messages, (mess) => { return mess.tempId === message.tempId });
        message.tempId = null;
        if(lastMessageIndex !== -1){
          this.chat.messages[lastMessageIndex] = message;
        }
        else{
          this.chat.messages.push(message);
        }
        if(!this.isSelected && message.userId !== this.user.id ){
          this.unreadCount += 1;
        }
    });

    this.chatHub.onConnected((connectionId: string, user: User) => {
      this.tosterService.info("HUB", `connected ${connectionId}`)
      //todo:change to set new user to online state;
      // this.messages.push({
      //   type: 'server', user: this.getUser(user), message: `user ${user.lastName} connected`
      // });
    });

    this.chatHub.onDisconnected((connectionId: string, user: User, error: string) => {
      this.tosterService.info("HUB", "disconnected")
      // this.messages.push({
      //   type: 'server', error: error, user: this.getUser(user), message: `user ${user.lastName} disconnected`
      // });
    });

    this.chatHub.onNewUserAdded((connectionId: string, user: User, error: string) => {
      this.tosterService.info("HUB", "disconnected")
      // this.messages.push({
      //   type: 'server', error: error, user: this.getUser(user), message: `user ${user.lastName} disconnected`
      // });
    });

    this.chatHub.connectToChat(this.chat.id).subscribe(
      sucsses => { this.tosterService.success("Success", `connectToChat! ${this.chat.id}`) },
      error => { this.tosterService.error() });
  }
}
