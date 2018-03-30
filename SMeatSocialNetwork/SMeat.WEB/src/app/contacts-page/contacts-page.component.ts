import { Component, OnInit } from '@angular/core';
import { Friend } from '../_models/friend';
import { UsersService } from '../_services/users.service';
import { BaseTosterService } from '../_services/base-toaster.service';

@Component({
  selector: 'app-contacts-page',
  templateUrl: './contacts-page.component.html',
  styleUrls: ['./contacts-page.component.css']
})
export class ContactsPageComponent implements OnInit {

  contacts: Friend[];

  contactPage: number = 0;
  contactCount: number = 100;
  contactSearchBy: string;

  currentUserId: string;

  constructor(private usersService: UsersService,
    private tosterService: BaseTosterService) { }

  ngOnInit() {
    this.getContacts();
  }

  getContacts() {
    this.usersService.getContacts(this.contactPage, this.contactCount, this.contactSearchBy)
      .subscribe(
        contacts => {
          this.contacts = contacts;
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
