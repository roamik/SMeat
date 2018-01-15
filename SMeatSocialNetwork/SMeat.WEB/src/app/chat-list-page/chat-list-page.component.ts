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
import * as moment from 'moment';

@Component({
  selector: 'app-chat-list-page',
  providers: [ ChatHub ],
  templateUrl: './chat-list-page.component.html',
  styleUrls: ['./chat-list-page.component.css']
})
export class ChatListPageComponent implements OnInit { 
  _chats: Chat[] = [];
  get chats() : Chat[]{    
    return this._chats.sort((left, right) => { 
      let lastLeft = _.last(left.messages) || null;
      var lastRight = _.last(right.messages) || null;
      return moment.utc(moment.utc(lastRight.dateTime)).diff(lastLeft.dateTime)
    })
  }
  // get chatsDates() : any[]{
  //   return _.map(this._chats, (chat)=> { var last = _.last(chat.messages); return last != null ? moment(last.dateTime).toDate() : null})
  //   .sort((left, right) => { return moment.utc(right).diff(moment.utc(left)) });
  // }
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
        //(chat:Chat) => { )
        this._chats = _.concat(chats, this._chats);
        this.selectedChat = _.first(this.chats);
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
         this.chatHub.sendMessage(message);
    }
}
