import { Component, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { User } from "../_models/user";
import { UsersService } from "../_services/users.service";
import { GenderType } from "../_enums/genders";
import { RelationshipType } from "../_enums/relations";

import { Board } from '../_models/board';
import { BoardsService } from "../_services/boards.service";
import { ContactsService } from '../_services/contacts.service';
import { AuthGuard } from '../_guards/auth.guard';

@Component({
  selector: 'profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  constructor(private route: ActivatedRoute,
    private usersService: UsersService,
    private guard: AuthGuard,
    private contactsService: ContactsService,
    private boardsService: BoardsService) { }

  id: string;
  private sub: any;
  currentUserId: string;
  isFriend: boolean;
  inRequest: boolean;

  public genders: typeof GenderType = GenderType;
  public relations: typeof RelationshipType = RelationshipType;

  user: User = new User();

  boards: Board[];

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.id = params['id']; // (+) converts string 'id' to a number

      this.getUserInfo(this.id);
    });
    this.currentUserId = this.guard.userId;
  }

  getUserInfo(id: string) {
    this.usersService.getById(id).subscribe(
      user => {
        this.user = user,
          this.getBoards(this.id),
          this.isFriend = user.isFriend;
        this.inRequest = user.inRequest;
      },
      error => { }
    )
  }

  onImageLoaded() { // update user info after modal method - uploadFile in image-modal.component finishes with success
    this.getUserInfo(this.id);
  }

  getBoards(id: string) {
    this.boardsService.getMyBoards(id)
      .subscribe(
      boards => {
        this.boards = boards;
      });
  }

  addContact(id: string) {
    this.contactsService.addContact(id).subscribe(
      success => {
        this.getUserInfo(this.id);
      },
      error => { }
    )
  }

  removeContact(id: string) {
    this.contactsService.removeContact(id).subscribe(
      user => {

      },
      error => { }
    )
  }
}
