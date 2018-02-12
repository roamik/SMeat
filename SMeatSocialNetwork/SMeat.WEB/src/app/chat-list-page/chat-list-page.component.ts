import { Component, OnInit, OnDestroy } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Chat, UserChat } from '../_models/chat';
import { BaseTosterService } from '../_services/base-toaster.service';
import { ChatsService } from '../_services/chats.service';
import { UsersService } from '../_services/users.service';
import { User } from '../_models/user';

import * as _ from "lodash";
import { ChatHub } from '../_hubs/chats.hub';
import { Message } from '../_models/message';
import { MessagesService } from '../_services/messages.service.';
import { Page } from '../_models/page';
import * as moment from 'moment';
import { UserStatusType } from '../_enums/user-status';

@Component({
  selector: 'app-chat-list-page',
  providers: [ChatHub],
  templateUrl: './chat-list-page.component.html',
  styleUrls: ['./chat-list-page.component.css']
})
export class ChatListPageComponent implements OnInit, OnDestroy {
  _chats: Chat[] = [];
  get chats(): Chat[] {
    return this._chats.sort((left, right) => {
      let lastLeft = _.last(left.messages) || null;
      let lastRight = _.last(right.messages) || null;
      return moment.utc(moment.utc(lastRight.dateTime)).diff(lastLeft.dateTime)
    })
  }
  selectedChat: Chat;
  user: User;
  chatsPage: number = 0;
  chatsCount: number = 50;
  chatsSearchBy: string;

  get selectedUserChat(): UserChat {
    if (this.selectedChat) {
      var userChatIndex = _.findIndex(this.selectedChat.userChats, (userChat) => { return userChat.userId === this.user.id });
      if (userChatIndex !== -1) {
        return this.selectedChat.userChats[userChatIndex];
      }
    }
    return null;
  }

  //loaders
  loaders: { [key: string]: boolean } = {
    "messages": false,
    "chats": false
  }

  loading: boolean = false;
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

  getUserChats() {
    this.loaders["chats"] = true;
    this.chatsService.getChats(this.chatsPage, this.chatsCount, this.chatsSearchBy)
      .subscribe(
      chats => {
        this._chats = _.concat(chats, this._chats);
        this.selectedChat = _.first(this.chats);
      },
      error => {
        this.tosterService.error("getUserChats");
      },
      () => this.loaders["chats"] = false);
  }

  getNextMessages(chat: Chat, page: Page) {
    this.loaders["messages"] = true;
    this.messagesService.getMessages(page.page, page.count, page.searchBy, chat.id, page.from)
      .subscribe(
      messages => {
        chat.messages = _.concat(messages, chat.messages);
      },
      error => {
        this.tosterService.error("getNextMessages");
      },
      () => this.loaders["messages"] = false);
  }

  changeStatus(status: UserStatusType) {
    this.chatHub.changeUserStatus(status, this.selectedChat.id);
  }

  sendMessage(message): void {
    this.chatHub.sendMessage(message);
  }

  ngOnDestroy(): void {
    this.chatHub.close();
  }
}
