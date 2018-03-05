import { Component, OnInit } from '@angular/core';

import { Board } from '../_models/board';
import { Reply } from '../_models/reply';
import { BoardsService } from "../_services/boards.service";
import { RouterModule, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-board-page',
  templateUrl: './board-page.component.html',
  styleUrls: ['./board-page.component.css']
})
export class BoardPageComponent implements OnInit {

    id: string;
    private sub: any;
    board: Board = new Board();
    replies: Reply[];

    constructor(private boardsService: BoardsService, private route: ActivatedRoute) { 
    }

    ngOnInit() {
      this.sub = this.route.params.subscribe(params => {
        this.id = params['id']; // (+) converts string 'id' to a number
  
        this.getBoardInfo(this.id);
      });
    }
  
    getBoardInfo(id: string) {
      this.boardsService.getById(id).subscribe(
        board => { this.board = board },
        error => { }
      )
    }
}
