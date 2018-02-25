import { Component, OnInit, Input } from "@angular/core";
import { Board } from "../_models/board";

@Component({
  selector: "app-board-view",
  templateUrl: "./board-view.component.html",
  styleUrls: ["./board-view.component.css"]
})

export class BoardViewComponent implements OnInit {
  @Input() board: Board;

  constructor() {
    
  }

  voyeUp(): boolean {
    this.board.voteUp();
    return false;
  }

  voteDown(): boolean {
    this.board.voteDown();
    return false;
  }

  ngOnInit() {
  }
}
