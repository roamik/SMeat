import { Component, OnInit, EventEmitter } from '@angular/core';

import { Board } from '../_models/board';
import { Reply } from '../_models/reply';
import { BoardsService } from "../_services/boards.service";
import { RepliesService } from "../_services/replies.service";
import { ActivatedRoute } from '@angular/router';

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
    newReply: Reply = new Reply();

    constructor(private repliesService: RepliesService, private boardsService: BoardsService, private route: ActivatedRoute) {
    }

    ngOnInit() {
      this.sub = this.route.params.subscribe(params => {
        this.id = params['id']; // (+) converts string 'id' to a number
  
        this.getBoardInfo(this.id);
        this.getReplies(this.id);
      });
    }

    scrollToId(id) {
      location.hash = id;
    }

    AddReplyTo(str) {
      this.newReply.replyId.push(str);
    }

    RemoveReplyTo(str) {
      let index = this.newReply.replyId.indexOf(str);
      this.newReply.replyId.splice(index, 1);
    }

    getBoardInfo(id: string) {
      this.boardsService.getById(id).subscribe(
        board => { this.board = board },
        error => { }
      )
    }

    getReplies(boardId: string) {
      this.repliesService.getByBoardId(boardId).subscribe(
        replies => { this.replies = replies },
        error => { }
      )
    }

    addReply() {
      this.newReply.boardId = this.id;
      this.repliesService.add(this.newReply)
        .subscribe(
        reply => {
          this.newReply.text = '';
          this.newReply.replyId = [];
          this.getReplies(this.id);
        },
        error => {
        });
    }
}
