import { Injectable } from "@angular/core";
import { ToasterService, ToasterConfig, Toast } from "angular2-toaster/angular2-toaster";
import { Router } from "@angular/router";

@Injectable()
export class CustomTosterService {

  private toasterService: ToasterService;

  public config1: ToasterConfig = new ToasterConfig({
    positionClass: 'toast-top-right'
  });

  constructor(private router: Router, toasterService: ToasterService) {
    this.toasterService = toasterService;
  }

  public popToastError() {
    var toast: Toast = {
      type: 'error',
      title: 'Incorrect data inserted!',
      body: 'Email or login is incorrect'
    };

    this.toasterService.pop(toast);
  }

  popToastSuccess() {
    var toast: Toast = {
      type: 'success',
      title: 'Success!',
      body: 'Applied succesfully!'
    };

    this.toasterService.popAsync(toast);
  }
}
