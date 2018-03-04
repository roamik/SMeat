import { Component, OnInit, Input } from '@angular/core';
import { Router } from "@angular/router";

import { Board } from '../_models/board';
import { BoardsService } from "../_services/boards.service";

@Component({
  selector: 'board-list-page',
  templateUrl: './board-list-page.component.html',
  styleUrls: ['./board-list-page.component.css']
})
export class BoardListPageComponent implements OnInit {

  @Input() boards: Board[]; 

  boardPage: number = 0;
  boardCount: number = 100;
  boardeSearchBy: string;

  constructor(private boardsService: BoardsService) {

  }

  ngOnInit() {
    this.getBoards();
  }

  getBoards() {
    this.boardsService.getBoards(this.boardPage, this.boardCount, this.boardeSearchBy)
      .subscribe(
      boards => {
        this.boards = boards;
      });
  }
}
