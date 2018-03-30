import { Component, OnInit, Input } from '@angular/core';
import { UsersService } from '../_services/users.service';
import { BaseTosterService } from '../_services/base-toaster.service';
import { Request } from '../_models/request';

@Component({
  selector: 'requests-page',
  templateUrl: './requests-page.component.html',
  styleUrls: ['./requests-page.component.css']
})
export class RequestsPageComponent implements OnInit {

  requests: Request[];

  requestPage: number = 0;
  requestCount: number = 100;
  requestSearchBy: string;

  currentUserId: string;

  constructor(private usersService: UsersService,
    private tosterService: BaseTosterService) { }

  ngOnInit() {
    this.getRequests();
  }

  getRequests() {
    this.usersService.getRequests(this.requestPage, this.requestCount, this.requestSearchBy)
      .subscribe(
      requests => {
        this.requests = requests;
      },
      error => {
      });
    this.usersService.getMyInfo()
      .subscribe(
      user => {
        this.currentUserId = user.id;
      },
      error => {
      });
  }

}
