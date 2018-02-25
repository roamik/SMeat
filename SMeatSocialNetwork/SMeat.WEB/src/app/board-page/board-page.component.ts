import { Component, OnInit } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { Board } from '../_models/board';

@Component({
  selector: 'app-board-page',
  templateUrl: './board-page.component.html',
  styleUrls: ['./board-page.component.css']
})
export class BoardPageComponent implements OnInit {

  board: Board;

  constructor() { 
    
  }

  ngOnInit() {
  }

}
