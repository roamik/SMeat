import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { User } from "../_models/user";
import { Location } from "../_models/location";
import { UsersService } from "../_services/users.service";
import { LocationsService } from "../_services/locations.service";
import { NgSelectModule } from '@ng-select/ng-select';
import { GenderType } from '../_enums/genders';
import { RelationshipType } from "../_enums/relations";
import { WorkPlace } from "../_models/workplace";
import { WorkplacesService } from "../_services/workplaces.service";
import { EnumToArrayHelper } from "../_helpers/EnumToArrayHelper";
import { BaseTosterService } from "../_services/base-toaster.service";

@Component({
  selector: 'app-user-settings-page',
  templateUrl: './user-settings-page.component.html',
  styleUrls: ['./user-settings-page.component.css']
})
export class UserSettingsPageComponent implements OnInit {

  constructor(private usersService: UsersService,
    private locationService: LocationsService,
    private workplaceService: WorkplacesService,
    private enumSelector: EnumToArrayHelper,
    private router: Router,
    private tosterService: BaseTosterService) {  }

  
  public locations: Array<Location> = [];

  public workplaces: Array<WorkPlace> = [];


  public genders: any = this.enumSelector.enumSelector(GenderType);

  public relations: any = this.enumSelector.enumSelector(RelationshipType);

  public colors: Array<string> = ['AppleGreen', 'OrangeFox', 'CheerryRed'];

  public templates: Array<string> = ['Classic', 'Extra'];

  private value: any = {};

  public typed(value: any): void {
    console.log('New search input: ', value);
  }

  public refreshValue(value: any): void {
    this.value = value;
  }

  locationPage: number = 0;
  locationCount: number = 100;
  locationSearchBy: string;

  workplacePage: number = 0;
  workplaceCount: number = 100;
  workplaceSearchBy: string;

  user: User = new User();

  ngOnInit() {
    this.getUserInfo();
    this.getLocations();
    this.getWorkplaces();
  }

  updateUserInfo() {
    this.usersService.update(this.user)
      .subscribe(
        success => {
          this.tosterService.success();
        },
        error => {
          this.tosterService.error();
        });
  }

  getUserInfo() {
    this.usersService.getMyInfo().subscribe(
      user => { this.user = user },
      error => { this.tosterService.error("getMyInfo"); }
    )
  }

  getLocations() {
    this.locationService.getLocations(this.locationPage, this.locationCount, this.locationSearchBy)
      .subscribe(
      locations => {
        this.locations = locations;
      },
      error => {
        this.tosterService.error("locations");
      });
  }

  getWorkplaces() {
    this.workplaceService.getWorkplaces(this.workplacePage, this.workplaceCount, this.workplaceSearchBy)
      .subscribe(
      workplaces => {
        this.workplaces = workplaces;
      },
      error => {
        this.tosterService.error("workplaces");
      });
  }
}
