import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes, Router } from '@angular/router';
import { ChatsService } from '../_services/chats.service';
import { Chat } from '../_models/chat';
import { AuthGuard } from '../_guards/auth.guard';

@Component({
  selector: 'chat-creation-page',
  templateUrl: './chat-creation-page.component.html',
  styleUrls: ['./chat-creation-page.component.css']
})
export class ChatCreationPageComponent implements OnInit {

  chat: Chat = new Chat();

  constructor(private chatService: ChatsService, private route: Router, private guard: AuthGuard) { }

  ngOnInit() {
    this.chat.picture = 'https://image.freepik.com/free-icon/group-of-people-in-a-formation_318-44341.jpg';
  }

  onImageLoaded(e) {
    this.chat.picture = e;
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
