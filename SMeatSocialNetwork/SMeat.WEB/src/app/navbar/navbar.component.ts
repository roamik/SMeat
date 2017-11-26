import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { AuthGuard } from '../_guards/auth.guard';
import { User } from "../_models/user";
import { UserService } from "../_services/users.service";

import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @ViewChild('sidenav') sidenav: any;

  constructor(private guard: AuthGuard, private userService: UserService) {
    this.guard.userStateChange.subscribe(
      state => this.getUserInfo(state)
    )
  }

  get isAuthenticated(): boolean { return this.guard.isAuthenticated() }

  get user(): User { return this._user; }

  _user: User = new User();

  ngOnInit() {

    this.getUserInfo(this.isAuthenticated);

  }

  getUserInfo(state) {
    
    if (state) {
      this.userService.getMyInfo().subscribe(
        user => { this._user = user },
        error => { }
      )
    }
    else {
      this._user = new User();
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    if (event.target.innerWidth < 500) {
      this.sidenav.close();
    }
  }

}
