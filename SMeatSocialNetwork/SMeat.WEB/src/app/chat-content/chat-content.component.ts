import { Component, OnInit, Input, SimpleChange, Output, EventEmitter } from '@angular/core';
import { Chat } from '../_models/chat';
import { ChatsService } from '../_services/chats.service';
import { BaseTosterService } from '../_services/base-toaster.service';
import * as _  from "lodash";
import { User } from '../_models/user';
import { Message } from '../_models/message';
import { Page } from '../_models/page';

@Component({
  selector: 'chat-content',
  templateUrl: './chat-content.component.html',
  styleUrls: ['./chat-content.component.css']
})
export class ChatContentComponent implements OnInit {
  constructor(private chatsService: ChatsService, private tosterService: BaseTosterService) { 

  }
  @Input() chat: Chat; 
  @Input() user: User; 
  @Input() loading: boolean;   
  @Output() newMessage = new EventEmitter<Message>();
  @Output() newPage = new EventEmitter<Page>();

  message: Message = new Message();
  page = new Page();
 
  ngOnChanges(changes: {[chat: string]: SimpleChange}) {
    
  }
  
  ngOnInit() {
   // this.getChatMessages();
  }

  sendMessage(){
    this.message.chatId = this.chat.id;
    this.message.userId = this.user.id;
    this.newMessage.emit(this.message)
    this.message = new Message();
  }

  onScrollUp () {
    this.page.from = _.first(this.chat.messages).dateTime;
    this.newPage.emit(this.page)
  }
}
