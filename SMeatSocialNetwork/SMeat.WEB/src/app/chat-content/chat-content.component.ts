import { Component, OnInit, Input, SimpleChange, Output, EventEmitter, ViewChild, ElementRef, SimpleChanges } from '@angular/core';
import { Chat } from '../_models/chat';
import { ChatsService } from '../_services/chats.service';
import { BaseTosterService } from '../_services/base-toaster.service';
import * as _  from "lodash";
import { User } from '../_models/user';
import { Message } from '../_models/message';
import { Page } from '../_models/page';
import { AfterViewChecked, AfterViewInit, OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { Subject } from 'rxjs/Subject';
import { Guid } from '../_helpers/GUID';
import { Random } from '../_helpers/random';
import { UserStatusType } from '../_enums/user-status';

@Component({
  selector: 'chat-content',
  templateUrl: './chat-content.component.html',
  styleUrls: ['./chat-content.component.css']
})
export class ChatContentComponent implements OnInit, AfterViewChecked {
 
  constructor(private chatsService: ChatsService, private tosterService: BaseTosterService) {   }
  @Input() chat: Chat; 
  @Input() user: User; 
  @Input() loading: boolean;   
  @Output() newMessage = new EventEmitter<Message>();
  @Output() newPage = new EventEmitter<Page>();

  onMessagesLoaded : Subject<any> = new Subject<any>()
  loaded = false;

  @ViewChild('endOfMessages') private endOfMessages: ElementRef;

  message: Message = new Message();
  page = new Page();
 
  ngOnChanges(changes: SimpleChanges) {
    if (changes['chat']){
      this.onMessagesLoaded.next(); //emit onMessagesLoaded event when chat is changed
    }
  }
  
  ngOnInit() {
    this.onMessagesLoaded.subscribe(
      (sucsess) => {
        this.scrollToBottom();//scroll to last message on onMessagesLoaded event
      }
    )
  } 
  
 
   get randomStatus(): UserStatusType {
    return Random.randomInt(0,3)
   }
  
  ngAfterViewChecked() {
    if(this.endOfMessages && this.endOfMessages.nativeElement && !this.loaded){
      this.onMessagesLoaded.next();
      this.loaded = true;
    }
  }
  
  scrollToBottom()  {  
    setTimeout(()=> {
      if(this.endOfMessages && this.endOfMessages.nativeElement){  
        this.endOfMessages.nativeElement.scrollIntoView({block: "end", behavior: "smooth"});  //scroll to last message
      }
    },0);
  }

  sendMessage(){
    this.message.chatId = this.chat.id;
    this.message.userId = this.user.id;
    this.message.tempId = Guid.newGuid();
    this.chat.messages.push(this.message);
    this.scrollToBottom();
       //scroll after elements added (js queue closure)
      
    this.newMessage.emit(this.message); // send message to parent component
    this.message = new Message(); // clear message
  }

  onScrollUp () {
    this.page.from = _.first(this.chat.messages).dateTime; // to get dataTime of oldest massage
    this.newPage.emit(this.page) // send page to parent component
  }
}
