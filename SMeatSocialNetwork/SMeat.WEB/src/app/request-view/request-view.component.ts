import { Component, OnInit, Input } from '@angular/core';
import { Request } from '../_models/request';
import { UsersService } from '../_services/users.service';
import { ContactsService } from '../_services/contacts.service';

@Component({
  selector: 'request-view',
  templateUrl: './request-view.component.html',
  styleUrls: ['./request-view.component.css']
})
export class RequestViewComponent implements OnInit {

  @Input() request: Request;

  @Input() currentUserId : string;

  constructor(private usersService: UsersService,
    private contactsService: ContactsService) { }

  ngOnInit() {

  }

  applyRequest(id: string) {
    this.contactsService.addContact(id).subscribe(
      user => {

      },
      error => { }
    )
  }
}
