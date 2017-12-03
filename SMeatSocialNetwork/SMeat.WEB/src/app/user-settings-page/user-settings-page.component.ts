import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { User } from "../_models/user";
import { Location } from "../_models/location";
import { UsersService } from "../_services/users.service";
import { LocationsService } from "../_services/locations.service";
import { NgSelectModule } from '@ng-select/ng-select';

@Component({
  selector: 'app-user-settings-page',
  templateUrl: './user-settings-page.component.html',
  styleUrls: ['./user-settings-page.component.css']
})
export class UserSettingsPageComponent implements OnInit {

  constructor(private usersService: UsersService, private locationService: LocationsService, private router: Router) { }

  public locations: Array<Location> = [];

  public workplaces: Array<string> = ['WorkPlace1', 'WorkPlace2', 'WorkPlace3', 'WorkPlace4',
    'WorkPlace5', 'WorkPlace6', 'WorkPlace7', 'WorkPlace8', 'WorkPlace9', 'WorkPlace10',
    'WorkPlace11', 'WorkPlace12', 'WorkPlace13'];

  public genders: Array<string> = ['Male', 'Female', 'Other'];

  public relationships: Array<string> = ['Single', 'In relation', 'Complicated', 'Waiting for a miracle'];

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

  user: User = new User();

  ngOnInit() {

    this.getUserInfo();
    this.getLocations();
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
}
