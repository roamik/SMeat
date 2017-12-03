import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { NgSelectModule } from '@ng-select/ng-select';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { AppComponent } from './app.component';

import { AppService } from './app.service';
import { AuthGuard } from './_guards/auth.guard';
import { AuthenticationService } from './_services/authentication.service';
import { UsersService } from './_services/users.service';
import { LocationsService } from './_services/locations.service';

import { HomePageComponent } from './home-page/home-page.component';
import { HomePageService } from './home-page/home-page.service';
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
import { UnsignedPageComponent } from './unsigned-page/unsigned-page.component';
import { NavbarComponent } from './navbar/navbar.component';

const appRoutes: Routes = [
  { path: 'home', component: HomePageComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginPageComponent },
  { path: 'registration', component: RegistrationPageComponent },
  { path: 'profile/:id', component: ProfilePageComponent, canActivate: [AuthGuard] },
  { path: 'settings', component: UserSettingsPageComponent, canActivate: [AuthGuard] },
  { path: 'chats/create', component: ChatCreationPageComponent, canActivate: [AuthGuard] },
  { path: 'groups/create', component: GroupCreationPageComponent, canActivate: [AuthGuard] },
  { path: 'boards/create', component: BoardCreationPageComponent, canActivate: [AuthGuard] },
  { path: 'chats', component: ChatListPageComponent, canActivate: [AuthGuard] },
  { path: 'groups', component: GroupListPageComponent, canActivate: [AuthGuard] },
  { path: 'boards', component: BoardListPageComponent, canActivate: [AuthGuard] },
  { path: 'groups/:id', component: GroupPageComponent, canActivate: [AuthGuard] },
  { path: 'chats/:id', component: ChatPageComponent, canActivate: [AuthGuard] },
  { path: 'boards/:id', component: BoardPageComponent, canActivate: [AuthGuard] },
  { path: 'unsigned', component: UnsignedPageComponent },


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
    BoardCreationPageComponent,
    UnsignedPageComponent,
    NavbarComponent
  ],
  imports: [
    RouterModule.forRoot(appRoutes),
    BrowserModule,
    HttpModule,

    BrowserAnimationsModule,
    MatSidenavModule,
    MatButtonModule,
    MatIconModule,
    AngularFontAwesomeModule,
    FormsModule,
    NgSelectModule
  ],
  providers: [
    AppService,
    HomePageService,
    AuthenticationService,
    AuthGuard,
    UsersService,
    LocationsService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
