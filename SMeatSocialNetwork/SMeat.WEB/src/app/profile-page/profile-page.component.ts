import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes, Router, ActivatedRoute } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';
import { User } from "../_models/user";
import { UserService } from "../_services/users.service";

@Component({
  selector: 'profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  constructor(private route: ActivatedRoute, private guard: AuthGuard, private userService: UserService) { }

  id: string;
  private sub: any;

  user: User = new User();

  ngOnInit() {

    this.sub = this.route.params.subscribe(params => {
      this.id = params['id']; // (+) converts string 'id' to a number

      this.getUserInfo(this.id);
    });
  }

  getUserInfo(id: string) {
    this.userService.getById(id).subscribe(
      user => { this.user = user },
      error => { }
    )
  }

}
