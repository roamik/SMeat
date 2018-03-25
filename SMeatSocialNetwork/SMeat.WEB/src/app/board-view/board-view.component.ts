import { Component, OnInit, Input } from "@angular/core";
import { Board } from "../_models/board";

import { RepliesService } from "../_services/replies.service";
import { BoardsService } from "../_services/boards.service";

@Component({
  selector: "app-board-view",
  templateUrl: "./board-view.component.html",
  styleUrls: ["./board-view.component.css"],
  providers: [RepliesService]
})

export class BoardViewComponent implements OnInit {
  @Input() board: Board;
  replyCount: number = 0;

  @Input() curUserId: number;

  constructor(private boardsService: BoardsService, private repliesService: RepliesService) {
    
  }

  ngOnInit() {
    this.getCount();
  }

  like(id: string) {
    this.boardsService.likeBoards(id)
      .subscribe(board => {
        this.board = board;
      });
  }
  dislike(id: string) {
    this.boardsService.dislikeBoards(id)
      .subscribe(board => {
        this.board = board;
      });
  }

  liked() {
    for (let i = 0; i < this.board.likes.length; i++) {
      if (this.board.likes[i].likeFromId === this.curUserId) return true;
    }
    return false;
  }
  disliked() {
    for (let i = 0; i < this.board.dislikes.length; i++) {
      if (this.board.dislikes[i].dislikeFromId === this.curUserId) return true;
    }
    return false;
  }

  getCount() {
    this.repliesService.getCountByBoardId(this.board.id)
      .subscribe(
      count => {
        this.replyCount = count;
        if (this.replyCount === null) this.replyCount = 0;
      });
  }
}
