import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';
import { ChatsService } from '../_services/chats.service';
import { Chat } from '../_models/chat';

@Component({
  selector: 'chat-creation-page',
  templateUrl: './chat-creation-page.component.html',
  styleUrls: ['./chat-creation-page.component.css']
})
export class ChatCreationPageComponent implements OnInit {

  chat: Chat = new Chat();

  constructor(private chatService: ChatsService, private route: Router) { }

  ngOnInit() {
  }

  addChat() {
    this.chatService.add(this.chat)
      .subscribe(
      chat => {
        this.chat = chat;
        this.route.navigate(['/chats']);
      },
      error => {
      });
  }

}
