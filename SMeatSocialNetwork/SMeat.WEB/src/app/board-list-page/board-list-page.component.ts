import { Component, OnInit, Input } from '@angular/core';
import { Router } from "@angular/router";

import { Board } from '../_models/board';
import { BoardsService } from "../_services/boards.service";
import { UsersService } from "../_services/users.service";

@Component({
  selector: 'board-list-page',
  templateUrl: './board-list-page.component.html',
  styleUrls: ['./board-list-page.component.css']
})
export class BoardListPageComponent implements OnInit {

  @Input() boards: Board[];

  curUserId: string;

  boardPage: number = 0;
  boardCount: number = 100;

  constructor(private boardsService: BoardsService, private usersService: UsersService) {

  }

  ngOnInit() {
    this.getUserInfo();
    this.getBoards();
  }

  getBoards(name: string = '') {
    this.boardsService.getBoards(this.boardPage, this.boardCount, name)
      .subscribe(
      boards => {
        this.boards = boards;
      });
  }
  
  getUserInfo() {
    this.usersService.getMyInfo().subscribe(
      user => {
        this.curUserId = user.id;
      }
    )
  }
}
