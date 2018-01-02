import { Component, OnInit } from '@angular/core';

import { Board } from '../_models/board';

@Component({
  selector: 'board-list-page',
  templateUrl: './board-list-page.component.html',
  styleUrls: ['./board-list-page.component.css']
})
export class BoardListPageComponent implements OnInit {

  boards: Board[]; 

  constructor() { }

  ngOnInit() {
  }

}
