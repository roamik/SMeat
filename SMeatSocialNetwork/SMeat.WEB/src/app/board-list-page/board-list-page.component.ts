import { Component, OnInit, Input } from '@angular/core';

import { Board } from '../_models/board';

@Component({
  selector: 'board-list-page',
  templateUrl: './board-list-page.component.html',
  styleUrls: ['./board-list-page.component.css']
})
export class BoardListPageComponent implements OnInit {

  @Input() boards: Board[]; 

  constructor() { 
    this.boards = [
      new Board('testID', 'test title', 'test text'),
      new Board('testID', 'test titlewwwwwwwwwwwwwwwwwwwwwwww wwwwwwwwwwwwwwwwwwwwwwwww', 'test text'),
      new Board('testID', 'test title', 'test text'),
      new Board('testID', 'test title sdsf sdfszef sefszefse', 'test text'),
      new Board('testID', 'test title 234124124', 'test text'),
      new Board('testID', 'test title2', 'test text2')
    ];
  }

  ngOnInit() {
  }

}
