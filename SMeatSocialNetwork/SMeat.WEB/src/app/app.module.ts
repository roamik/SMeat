import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AppService } from './app.service';

import { HomePageComponent } from './home-page/home-page.component';
import {HomePageService} from './home-page/home-page.service';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent
  ],
  imports: [  
    BrowserModule,
    HttpModule
  ],
  providers: [
      AppService,
      HomePageService
  ],
  bootstrap: [
      AppComponent,
      HomePageComponent
  ]
})
export class AppModule { }