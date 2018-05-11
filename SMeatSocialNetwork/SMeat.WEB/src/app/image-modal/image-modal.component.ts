import { Component, OnInit, EventEmitter, Output, ViewChild, TemplateRef } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'image-modal',
  templateUrl: './image-modal.component.html',
  styleUrls: ['./image-modal.component.css']
})
export class ImageModalComponent implements OnInit {

  location: Location = new Location();

  modalRef: BsModalRef;

  @ViewChild('template')
  template: TemplateRef<any>;


  @Output()
  change: EventEmitter<Location> = new EventEmitter<Location>();

  constructor(private modalService: BsModalService) { }

  ngOnInit() {
  }

  open() {
    this.modalRef = this.modalService.show(this.template);
  }

}
