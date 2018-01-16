import { Component, OnInit, TemplateRef, ViewChild, EventEmitter, Output } from '@angular/core';
import { BsModalService, BsModalRef } from "ngx-bootstrap";
import { LocationsService } from "../_services/locations.service";
import { Location } from "../_models/location";


@Component({
  selector: 'location-modal',
  templateUrl: './location-modal.component.html',
  styleUrls: ['./location-modal.component.css']
})
export class LocationModalComponent implements OnInit {

  location: Location = new Location();

  modalRef: BsModalRef;

  @ViewChild('template')
  template: TemplateRef<any>;


  @Output()
  change: EventEmitter<Location> = new EventEmitter<Location>();

  constructor(private modalService: BsModalService,
    private locationService: LocationsService) { }

  ngOnInit() {
  }

   open() {
    this.modalRef = this.modalService.show(this.template);
  }

  addLocation() {

    this.locationService.add(this.location)
      .subscribe(
      location => {
        this.location = location;
        this.modalRef.hide();
        this.change.emit(this.location);
        this.location = new Location();
      },
      error => {
      });
  }

}
