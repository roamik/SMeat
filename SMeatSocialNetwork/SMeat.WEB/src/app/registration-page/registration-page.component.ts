import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes, Router, ActivatedRoute } from '@angular/router';
import { Http, Response, RequestOptions, RequestOptionsArgs, Headers } from '@angular/http';

import { UserService } from '../_services/users.service';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css']
})

export class RegistrationPageComponent implements OnInit {

	model: any = {};
	loading = false;
	returnUrl: string;

    constructor(
        private http: Http,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService) {}

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
