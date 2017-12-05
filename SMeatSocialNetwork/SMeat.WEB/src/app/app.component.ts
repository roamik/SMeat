import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { TranslateService } from '@ngx-translate/core';

import { AppService } from './app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  pageTitle: string = '';
  errorMessage: string = '';

  constructor(private _appService: AppService, translate: TranslateService) {
    // this language will be used as a fallback when a translation isn't found in the current language
    translate.setDefaultLang('en');

    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use('en');
  }

  ngOnInit(): void {
    //this.showHello();
  }



  showHello(): void {
    this._appService.sayHello()
      .subscribe(
      result => {
        this.pageTitle = result;
      },
      error => {
        this.errorMessage = <any>error
      });
  }
}
