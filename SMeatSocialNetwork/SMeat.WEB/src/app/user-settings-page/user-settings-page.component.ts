import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { User } from "../_models/user";
import { Location } from "../_models/location";
import { UsersService } from "../_services/users.service";
import { LocationsService } from "../_services/locations.service";
import { NgSelectModule } from '@ng-select/ng-select';
import { GenderType } from '../_models/genders';
import { RelationshipType } from "../_models/relations";
import { WorkPlace } from "../_models/workplace";
import { WorkplacesService } from "../_services/workplaces.service";

@Component({
  selector: 'app-user-settings-page',
  templateUrl: './user-settings-page.component.html',
  styleUrls: ['./user-settings-page.component.css']
})
export class UserSettingsPageComponent implements OnInit {

  constructor(private usersService: UsersService, private locationService: LocationsService, private workplaceService: WorkplacesService, private router: Router) { }

  public locations: Array<Location> = [];

  public workplaces: Array<WorkPlace> = [];


  public genders: any = this.enumSelector(GenderType);

  public relations: any = this.enumSelector(RelationshipType);

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
      result => {
        this.router.navigate(['/profile', this.user.id]);
      },
      error => {
      });
  }

  getUserInfo() {

    this.usersService.getMyInfo().subscribe(
      user => { this.user = user },
      error => { }
    )
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

  getWorkplaces() {
    this.workplaceService.getWorkplaces(this.workplacePage, this.workplaceCount, this.workplaceSearchBy)
      .subscribe(
      workplaces => {
        this.workplaces = workplaces;
      },
      error => {
      });
  }

  enumSelector(definition) {
    var selectors = Object.keys(definition);

    return selectors.slice(selectors.length / 2).map(key => ({ value: definition[key], title: key }));
  }
}
