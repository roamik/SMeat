import { Component, OnInit, Input } from '@angular/core';
import { Request } from '../_models/request';
import { UsersService } from '../_services/users.service';

@Component({
  selector: 'request-view',
  templateUrl: './request-view.component.html',
  styleUrls: ['./request-view.component.css']
})
export class RequestViewComponent implements OnInit {

  @Input() request: Request;

  //@Input() currentUserId : any;

  constructor(private usersService: UsersService) { }

  ngOnInit() {

  }

  applyRequest(id: string) {
    this.usersService.confirmContact(id).subscribe(
      user => {

      },
      error => { }
    )
  }
}
