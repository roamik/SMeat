import { Injectable } from "@angular/core";
import { ToasterService, ToasterConfig, Toast } from "angular2-toaster/angular2-toaster";
import { Router } from "@angular/router";

@Injectable()
export class BaseTosterService {
  public baseToasterConfig: ToasterConfig = new ToasterConfig({
    positionClass: 'toast-top-right'
  });

  constructor(private router: Router, private toasterService: ToasterService) {    
  }

  public error(title = "Error!", body = "An error occured!") {
    var toast: Toast = {
      type: 'error',
      title: title,
      body: body
    };

    this.toasterService.pop(toast);
  }

  public success(title = "Success!", body = 'Applied succesfully!') {
    var toast: Toast = {
      type: 'success',
      title: title,
      body: body
    };
    this.toasterService.popAsync(toast);
  }

  public info(title = "Info!", body = 'Info!') {
    var toast: Toast = {
      type: 'info',
      title: title,
      body: body
    };
    this.toasterService.popAsync(toast);
  }
}
