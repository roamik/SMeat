import { Component, OnInit, Input } from "@angular/core";
import { Board } from "../_models/board";

import { RepliesService } from "../_services/replies.service";

@Component({
  selector: "app-board-view",
  templateUrl: "./board-view.component.html",
  styleUrls: ["./board-view.component.css"],
  providers: [RepliesService]
})

export class BoardViewComponent implements OnInit {
  @Input() board: Board;
  replyCount: number = 0;

  constructor(private repliesService: RepliesService) {
    
  }

  ngOnInit() {
    this.getCount();
  }

  voyeUp(): boolean {
    this.board.voteUp();
    return false;
  }

  voteDown(): boolean {
    this.board.voteDown();
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
