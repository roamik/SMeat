import { Component, OnInit, Input } from '@angular/core';

import { Reply } from '../_models/reply';

@Component({
  selector: 'app-reply-view',
  templateUrl: './reply-view.component.html',
  styleUrls: ['./reply-view.component.css']
})
export class ReplyViewComponent implements OnInit {

  @Input() reply: Reply;

  constructor() { }

  ngOnInit() {
  }

}
