import { Component, OnInit, EventEmitter, Output, ViewChild, TemplateRef, Input } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { ContactsService } from '../_services/contacts.service';
import { Friend } from '../_models/friend';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'contacts-picker-modal',
  templateUrl: './contacts-picker-modal.component.html',
  styleUrls: ['./contacts-picker-modal.component.css']
})
export class ContactsPickerModalComponent implements OnInit {

  constructor(private modalService: BsModalService,
    private contactsService: ContactsService,
    private authService: AuthenticationService) { }

  @ViewChild('template')
  template: TemplateRef<any>;
  modalRef: BsModalRef;

  @Output()
  confirm: EventEmitter<any> = new EventEmitter<any>();

  curUser: User;
  users: Friend[];
  pickedUsersIds: string[];

  page: number = 0;
  count: number = 500;
  searchString: string = '';

  ngOnInit() {
    this.curUser = JSON.parse(localStorage.getItem('currentUser')).id;
    this.pickedUsersIds = [];
    this.getContacts();
  }

  isPicked(id: string) {
    for (var i = 0; i < this.pickedUsersIds.length; i++)
      if (this.pickedUsersIds[i] === id)
        return true;
    return false;
  }

  tooglePicked(id: string) {
    var index = this.pickedUsersIds.indexOf(id);
    if (index === -1)
      this.pickedUsersIds.push(id);
    else
      this.pickedUsersIds.splice(index, 1);
  }

  confirmForm() {
    this.confirm.emit(this.pickedUsersIds);
  }

  getContacts() {
    this.contactsService.getContacts(this.page, this.count, this.searchString)
      .subscribe(users => {
        this.users = users;
      });
  }

  open() {
    this.modalRef = this.modalService.show(this.template);
  }
}
