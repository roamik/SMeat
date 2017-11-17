import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { AppService } from './app.service';

import { HomePageComponent } from './home-page/home-page.component';
import {HomePageService} from './home-page/home-page.service';
import { LoginPageComponent } from './login-page/login-page.component';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { RegistrationPageComponent } from './registration-page/registration-page.component';
import { UserSettingsPageComponent } from './user-settings-page/user-settings-page.component';
import { ChatCreationPageComponent } from './chat-creation-page/chat-creation-page.component';
import { ChatListPageComponent } from './chat-list-page/chat-list-page.component';
import { GroupListPageComponent } from './group-list-page/group-list-page.component';
import { GroupPageComponent } from './group-page/group-page.component';
import { BoardListPageComponent } from './board-list-page/board-list-page.component';
import { ChatPageComponent } from './chat-page/chat-page.component';
import { GroupCreationPageComponent } from './group-creation-page/group-creation-page.component';
import { BoardPageComponent } from './board-page/board-page.component';
import { BoardCreationPageComponent } from './board-creation-page/board-creation-page.component';


const appRoutes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'login-page', component: LoginPageComponent },
  { path: 'registration-page', component: RegistrationPageComponent },
  { path: 'profile/:id', component: ProfilePageComponent },
  { path: 'user-settings', component: UserSettingsPageComponent },
  { path: 'chat-list/createchat', component: ChatCreationPageComponent },
  { path: 'group-list/creategroup', component: GroupCreationPageComponent },
  { path: 'chat-list', component: ChatListPageComponent },
  { path: 'group-list', component: GroupListPageComponent },
  { path: 'board-feed', component: BoardListPageComponent },
  { path: 'groups/:id', component: GroupPageComponent },
  { path: 'chats/:id', component: ChatPageComponent },
  { path: 'boards/:id', component: BoardPageComponent },
  { path: 'board-feed/createboard', component: BoardCreationPageComponent },


  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  }
];

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LoginPageComponent,
	  RegistrationPageComponent,
    ProfilePageComponent,
    UserSettingsPageComponent,
    ChatCreationPageComponent,
    ChatListPageComponent,
    GroupListPageComponent,
    GroupPageComponent,
    BoardListPageComponent,
    ChatPageComponent,
    GroupCreationPageComponent,
    BoardPageComponent,
    BoardCreationPageComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    HttpModule
  ],
  providers: [
      AppService,
      HomePageService
  ],
  bootstrap: [
      AppComponent
  ]
})
export class AppModule { }
