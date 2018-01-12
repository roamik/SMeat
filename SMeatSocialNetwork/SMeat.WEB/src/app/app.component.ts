import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { TranslateService } from '@ngx-translate/core';

import { AppService } from './app.service';
import { BaseTosterService } from './_services/base-toaster.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  pageTitle: string = '';
  errorMessage: string = '';
  baseToasterConfig:{};

  constructor(private _appService: AppService, translate: TranslateService, private tosterService: BaseTosterService) {
    // this language will be used as a fallback when a translation isn't found in the current language
    translate.setDefaultLang('en');
    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use('en');
    this.baseToasterConfig = this.tosterService.baseToasterConfig;
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
