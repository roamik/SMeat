import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { BoardsService } from "../_services/boards.service";
import { Board } from "../_models/board";

@Component({
  selector: 'board-creation-page',
  templateUrl: './board-creation-page.component.html',
  styleUrls: ['./board-creation-page.component.css']
})
export class BoardCreationPageComponent implements OnInit {

    board: Board = new Board();

    constructor(private boardsService: BoardsService) {
    }

    ngOnInit() {
    }

    addBoard() {
        this.boardsService.add(this.board)
            .subscribe(
            board => {
                this.board = board;
                this.board = new Board();
            },
            error => {
            });
    }
}
