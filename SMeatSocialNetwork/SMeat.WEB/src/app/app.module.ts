
//modules
import { AngularFontAwesomeModule } from "angular-font-awesome";
import { BadrequestInterceptor } from "./_interceptors/badrequest.interceptor";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { BrowserModule } from "@angular/platform-browser";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from "@angular/common/http";
import { HttpModule } from "@angular/http";
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatSidenavModule } from "@angular/material/sidenav";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalModule, BsModalService } from "ngx-bootstrap";
import { NgModule } from "@angular/core";
import { NgSelectModule } from "@ng-select/ng-select";
import { RouterModule, Routes } from "@angular/router";
import { ToasterModule, ToasterService } from "angular2-toaster";
import { TokenInterceptor } from "./_interceptors/token.interceptor";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { UnauthorizeInterceptor } from "./_interceptors/unauthorize.interceptor";

//services
import { AppService } from "./app.service";
import { AuthGuard } from "./_guards/auth.guard";
import { AuthenticationService } from "./_services/authentication.service";
import { UsersService } from "./_services/users.service";
import { LocationsService } from "./_services/locations.service";
import { BoardsService } from "./_services/boards.service";
import { RepliesService } from "./_services/replies.service";
import { WorkplacesService } from "./_services/workplaces.service";
import { ChatsService } from "./_services/chats.service";
import { MessagesService } from "./_services/messages.service.";
import { HomePageService } from "./home-page/home-page.service";
import { BaseTosterService } from "./_services/base-toaster.service";
import { ContactsService } from "./_services/contacts.service";
import { UploadService } from "./_services/upload.service";

//hubs
import { ChatHub } from "./_hubs/chats.hub";

//helpers
import { EnumToArrayHelper } from "./_helpers/EnumToArrayHelper";

//componets
import { AppComponent } from "./app.component";
import { BoardCreationPageComponent } from "./board-creation-page/board-creation-page.component";
import { BoardListPageComponent } from "./board-list-page/board-list-page.component";
import { BoardPageComponent } from "./board-page/board-page.component";
import { BoardViewComponent } from "./board-view/board-view.component";
import { ChatContentComponent } from './chat-content/chat-content.component';
import { ChatCreationPageComponent } from "./chat-creation-page/chat-creation-page.component";
import { ChatListPageComponent } from "./chat-list-page/chat-list-page.component";
import { ChatPageComponent } from "./chat-page/chat-page.component";
import { ChatPreviewComponent } from './chat-preview/chat-preview.component';
import { GroupCreationPageComponent } from "./group-creation-page/group-creation-page.component";
import { GroupListPageComponent } from "./group-list-page/group-list-page.component";
import { GroupPageComponent } from "./group-page/group-page.component";
import { HomePageComponent } from "./home-page/home-page.component";
import { LoginPageComponent } from "./login-page/login-page.component";
import { NavbarComponent } from "./navbar/navbar.component";
import { ProfilePageComponent } from "./profile-page/profile-page.component";
import { RegistrationPageComponent } from "./registration-page/registration-page.component";
import { UnsignedPageComponent } from "./unsigned-page/unsigned-page.component";
import { UserSettingsPageComponent } from "./user-settings-page/user-settings-page.component";
import { ContactsPageComponent } from './contacts-page/contacts-page.component';
import { UsersPageComponent } from './users-page/users-page.component';
import { ImagesPageComponent } from './images-page/images-page.component';

//pipes
import { MomentFormatPipe, MomentCalendarPipe } from './_pipes/moment.pipe';

import { LocationModalComponent } from "./location-modal/location-modal.component";
import { WorkplaceModalComponent } from "./workplace-modal/workplace-modal.component";
import { ReplyViewComponent } from './reply-view/reply-view.component';
import { RequestsPageComponent } from './requests-page/requests-page.component';
import { RequestViewComponent } from './request-view/request-view.component';
import { ContactViewComponent } from './contact-view/contact-view.component';
import { UserViewComponent } from './user-view/user-view.component';
import { ImageModalComponent } from './image-modal/image-modal.component';
import { ContactsPickerModalComponent } from './contacts-picker-modal/contacts-picker-modal.component';

const appRoutes: Routes = [
  { path: "home", component: HomePageComponent, canActivate: [AuthGuard] },
  { path: "login", component: LoginPageComponent },
  { path: "registration", component: RegistrationPageComponent },
  { path: "profile/:id", component: ProfilePageComponent, canActivate: [AuthGuard] },
  { path: "settings", component: UserSettingsPageComponent, canActivate: [AuthGuard] },
  { path: "chats/create", component: ChatCreationPageComponent, canActivate: [AuthGuard] },
  { path: "groups/create", component: GroupCreationPageComponent, canActivate: [AuthGuard] },
  { path: "boards/create", component: BoardCreationPageComponent, canActivate: [AuthGuard] },
  { path: "chats", component: ChatListPageComponent, canActivate: [AuthGuard] },
  { path: "chats/create", component: ChatCreationPageComponent, canActivate: [AuthGuard] },
  { path: "groups", component: GroupListPageComponent, canActivate: [AuthGuard] },
  { path: "boards", component: BoardListPageComponent, canActivate: [AuthGuard] },
  { path: "groups/:id", component: GroupPageComponent, canActivate: [AuthGuard] },
  { path: "chats/:id", component: ChatPageComponent, canActivate: [AuthGuard] },
  { path: "boards/:id", component: BoardPageComponent, canActivate: [AuthGuard] },
  { path: "unsigned", component: UnsignedPageComponent },
  { path: "requests", component: RequestsPageComponent, canActivate: [AuthGuard] },
  { path: "contacts", component: ContactsPageComponent, canActivate: [AuthGuard] },
  { path: "users/:name", component: UsersPageComponent, canActivate: [AuthGuard] },
  { path: "images", component: ImagesPageComponent, canActivate: [AuthGuard] },
  {
    path: "",
    redirectTo: "/home",
    pathMatch: "full"
  }
];

// AoT requires an exported function for factories
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}


@NgModule({
  declarations: [
    AppComponent,
    BoardCreationPageComponent,
    BoardListPageComponent,
    BoardPageComponent,
    BoardViewComponent,
    ChatContentComponent,
    ChatCreationPageComponent,
    ChatListPageComponent,
    ChatPageComponent,
    ChatPreviewComponent,
    GroupCreationPageComponent,
    GroupListPageComponent,
    GroupPageComponent,
    HomePageComponent,
    LoginPageComponent,
    MomentCalendarPipe,
    MomentFormatPipe,
    NavbarComponent,
    ProfilePageComponent,
    RegistrationPageComponent,
    UnsignedPageComponent,
    UserSettingsPageComponent,
    LocationModalComponent,
    WorkplaceModalComponent,
    ReplyViewComponent,
    RequestsPageComponent,
    RequestViewComponent,
    ContactsPageComponent,
    ContactViewComponent,
    UsersPageComponent,
    UserViewComponent,
    ImageModalComponent,
    ImagesPageComponent,
    ContactsPickerModalComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    HttpModule,
    ModalModule.forRoot(),
    InfiniteScrollModule,
    MatButtonModule,
    MatIconModule,
    MatSidenavModule,
    NgbModule.forRoot(),
    NgSelectModule,
    RouterModule.forRoot(appRoutes),
    ToasterModule,
    TranslateModule.forRoot(
      {
        loader:
        {
          provide: TranslateLoader,
          useFactory: HttpLoaderFactory,
          deps: [HttpClient]
        }
      })
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: UnauthorizeInterceptor,
    multi: true
  },
  {
    provide: HTTP_INTERCEPTORS,
    useClass: BadrequestInterceptor,
    multi: true
  },
    AppService,
    AuthenticationService,
    AuthGuard,
    BaseTosterService,
    ChatsService,
    EnumToArrayHelper,
    HomePageService,
    BoardsService,
    LocationsService,
    MessagesService,
    UsersService,
    WorkplacesService,
    BsModalService,
    RepliesService,
    BoardsService,
    ContactsService,
    UploadService
    //ChatHub
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
