import { Component, OnInit } from '@angular/core';

import { HomePageService } from './home-page.service';

@Component({
  selector: 'home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

    public test: any = {};
    public values: Array<any> = [];
    errorMessage: string = '';
    constructor(private _homePageService: HomePageService) {
    }

  ngOnInit() {

      this.getValues();

  }

  getValues(): void {
      this._homePageService.getValues()
          .subscribe(
          result => {
              this.values = result;
              this.test = result;
          },
          error => {
              this.errorMessage = <any>error
          });
  }

}
