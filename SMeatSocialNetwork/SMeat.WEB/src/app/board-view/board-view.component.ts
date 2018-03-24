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

  liked: boolean;
  disliked: boolean;

  constructor(private boardsService: BoardsService, private repliesService: RepliesService) {
    
  }

  ngOnInit() {
    this.getCount();
    this.checkLikesDislikes();
  }

  like(id: string) {
    this.boardsService.likeBoards(id)
      .subscribe(board => {
        this.board = board;
        this.liked = true;
      });
  }
  dislike(id: string) {
    this.boardsService.dislikeBoards(id)
      .subscribe(board => {
        this.board = board;
        this.disliked = true;
      });
  }

  checkLikesDislikes() {

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
