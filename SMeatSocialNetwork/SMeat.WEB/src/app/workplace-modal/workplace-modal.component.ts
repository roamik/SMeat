import { Component, OnInit, TemplateRef, ViewChild, EventEmitter, Output } from '@angular/core';
import { BsModalService, BsModalRef } from "ngx-bootstrap";
import { WorkplacesService } from "../_services/workplaces.service";
import { WorkPlace } from "../_models/workplace";
import { LocationsService } from "../_services/locations.service";
import { Location } from "../_models/location";


@Component({
  selector: 'workplace-modal',
  templateUrl: './workplace-modal.component.html',
  styleUrls: ['./workplace-modal.component.css']
})
export class WorkplaceModalComponent implements OnInit {

  workplace: WorkPlace = new WorkPlace();

  public locations: Array<Location> = [];

  locationPage: number = 0;
  locationCount: number = 100;
  locationSearchBy: string;

  modalRef: BsModalRef;

  @ViewChild('template')
  template: TemplateRef<any>;


  @Output()
  change: EventEmitter<WorkPlace> = new EventEmitter<WorkPlace>();

  constructor(private modalService: BsModalService,
    private workplaceService: WorkplacesService,
    private locationService: LocationsService) { }

  ngOnInit() {
    this.getLocations();
  }

  open() {
    this.modalRef = this.modalService.show(this.template);
  }

  getLocations() {
    this.locationService.getLocations(this.locationPage, this.locationCount, this.locationSearchBy)
      .subscribe(
      locations => {
        this.locations = locations;
      },
      error => {
      });
  }

  addWorkplace() {

    this.workplaceService.add(this.workplace)
      .subscribe(
      workplace => {
        this.workplace = workplace;
        this.modalRef.hide();
        this.change.emit(this.workplace);
        this.workplace = new WorkPlace();
      },
      error => {
      });
  }

}
