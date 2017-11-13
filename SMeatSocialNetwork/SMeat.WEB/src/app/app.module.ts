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


const appRoutes: Routes = [
  { path: 'home', component: HomePageComponent },
  { path: 'login-page', component: LoginPageComponent },
  { path: 'registration-page', component: RegistrationPageComponent },
  { path: 'profile-page', component: ProfilePageComponent },


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
    ProfilePageComponent
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
