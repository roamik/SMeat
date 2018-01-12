import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Chat } from '../_models/chat';
import { BaseTosterService } from '../_services/base-toaster.service';
import { ChatsService } from '../_services/chats.service';
import { UsersService } from '../_services/users.service';
import { User } from '../_models/user';

import * as _  from "lodash";
import { ChatHub } from '../_hubs/chats.hub';
import { Message } from '../_models/message';
import { MessagesService } from '../_services/messages.service.';
import { Page } from '../_models/page';

@Component({
  selector: 'app-chat-list-page',
  providers: [ ChatHub ],
  templateUrl: './chat-list-page.component.html',
  styleUrls: ['./chat-list-page.component.css']
})
export class ChatListPageComponent implements OnInit { 
  chats: Chat[] = [];
  selectedChat: Chat;
  user: User;
  loading: boolean = false;
  chatsPage: number = 0;
  chatsCount: number = 50;
  chatsSearchBy: string;

  constructor(private chatsService: ChatsService, private messagesService: MessagesService,
     private usersService: UsersService, private tosterService: BaseTosterService, private chatHub: ChatHub) { }

  ngOnInit() {
    this.getUserChats();
    this.getUserInfo(); 
    
    this.chatHub.started().subscribe(
      sucsses => {
        this.tosterService.info("HUB main", "started")
      },
      error => { this.tosterService.error("HUB main", "not started") }
    )
    this.chatHub.start();
  }

  getUserInfo() {
    this.usersService.getMyInfo().subscribe(
      user => { this.user = user },
      error => { this.tosterService.error("getMyInfo"); }
    )
  }

  getUserChats(){
    this.chatsService.getChats(this.chatsPage, this.chatsCount, this.chatsSearchBy)
    .subscribe(
      chats => {
        //(chat:Chat) => { var last = _.last(chat.messages); return last != null ? last.dateTime : null })
        this.chats = _.sortBy(_.concat(chats, this.chats));
        this.selectedChat = _.last(this.chats);
      },
      error => {
        this.tosterService.error("getUserChats");
      });
  }

  getNextMessages(chat:Chat, page:Page){
    this.loading = true;
    this.messagesService.getMessages(page.page, page.count, page.searchBy, chat.id, page.from)
    .subscribe(
      messages => {
          this.loading = false;
          chat.messages = _.concat(messages, chat.messages);
      },
      error => {
        this.loading = false;
        this.tosterService.error("getNextMessages");
      });
  }
  

  sendMessage(message): void {
         this.chatHub.sendMessage(message).subscribe(
             sucsses => { this.tosterService.success("Success", "Message send!") },
             error => { this.tosterService.error() }
         )
    }
}
