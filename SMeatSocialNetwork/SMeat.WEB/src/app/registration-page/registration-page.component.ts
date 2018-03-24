import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes, Router, ActivatedRoute } from '@angular/router';
import { Http, Response, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';

import { AuthenticationService } from '../_services/authentication.service';
import { EnumToArrayHelper } from '../_helpers/EnumToArrayHelper';
import { GenderType } from '../_enums/genders';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css']
})

export class RegistrationPageComponent implements OnInit {

  model: any = {};
  loading = false;
  returnUrl: string;

  public genders: any = this.enumSelector.enumSelector(GenderType);

  constructor(
    private enumSelector: EnumToArrayHelper,
    private http: Http,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  checkReg() {
    if (this.model.password === this.model.passwordRep) this.register();
    this.model.passwordRepError = true;
  }

  register() {
    this.loading = true;
    this.authenticationService.register(this.model)
      .subscribe(
      data => {
        this.router.navigate(['/home']);
      },
      error => {
        this.loading = false;
      });
  }

  ngOnInit() {
  }

}
