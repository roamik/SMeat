import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Reply } from '../_models/reply';
import { RepliesService } from '../_services/replies.service';

@Component({
  selector: 'app-reply-view',
  templateUrl: './reply-view.component.html',
  styleUrls: ['./reply-view.component.css']
})
export class ReplyViewComponent implements OnInit {

  @Input() reply: Reply;
  @Input() nth: number;

  replies: string[] = [];

  @Output() replyClicked = new EventEmitter();
  @Output() addReplytoId = new EventEmitter();
  @Output() removeReplytoId = new EventEmitter();

  constructor(private repliesService: RepliesService) { }

  ngOnInit() {
    this.getReplies();
  }

  checkReplyId(event)
  {
    switch(event.target.checked)
    {
      case true:
        this.addReplytoId.emit(this.reply.id);
        break;
      case false:
        this.removeReplytoId.emit(this.reply.id);
        break;
    }
  }

  getReplies()
  {
    this.repliesService.getRepliedAt(this.reply.id)
      .subscribe(
      replies => {
        this.replies = replies;
      });
  }

}
