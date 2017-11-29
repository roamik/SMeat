import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { User } from "../_models/user";
import { UserService } from "../_services/users.service";

@Component({
  selector: 'app-user-settings-page',
  templateUrl: './user-settings-page.component.html',
  styleUrls: ['./user-settings-page.component.css']
})
export class UserSettingsPageComponent implements OnInit {

  constructor(private userService: UserService, private router: Router) { }

  user: User = new User();

  ngOnInit() {

    this.getUserInfo();

  }

  updateUserInfo() {

    this.userService.update(this.user)
      .subscribe(
      result => {
        this.router.navigate(['/profile', this.user.id]);
      },
      error => {
      });
  }

  getUserInfo() {

    this.userService.getMyInfo().subscribe(
      user => { this.user = user },
      error => { }
    )
  }
}
