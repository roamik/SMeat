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


const appRoutes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'login-page', component: LoginPageComponent },
  { path: 'registration-page', component: RegistrationPageComponent },
  { path: 'profile', component: ProfilePageComponent },
  { path: 'user-settings', component: UserSettingsPageComponent },
  { path: 'chat-creation', component: ChatCreationPageComponent },
  { path: 'chat-list', component: ChatListPageComponent },


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
    ChatListPageComponent
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
