import { Component, OnInit } from '@angular/core';
import { UsersService } from '../_services/users.service';
import { User } from '../_models/user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'users-page',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.css']
})
export class UsersPageComponent implements OnInit {

  users: Array<User> = [];

  length: number;
  currentPage: number = 0;
  usersCount: number = 10;
  search: string;

  constructor(private usersService: UsersService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => this.getUsers(this.currentPage, params['name']));
  }

  getUsers(page, searchBy) {
    this.currentPage = page;
    this.usersService.getUsers(this.currentPage, this.usersCount, searchBy)
      .subscribe(
      pageModel => {
        this.users = pageModel.items;
      },
      error => { }
      );
  }

}
