import { Component, OnInit, Input } from '@angular/core';
import { Friend } from '../_models/friend';

@Component({
  selector: 'contact-view',
  templateUrl: './contact-view.component.html',
  styleUrls: ['./contact-view.component.css']
})
export class ContactViewComponent implements OnInit {

  @Input() contact: Friend;

  constructor() { }

  ngOnInit() {
  }

}
