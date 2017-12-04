import { Component, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { User } from "../_models/user";
import { UsersService } from "../_services/users.service";
import { GenderType } from "../_models/genders";
import { RelationshipType } from "../_models/relations";

@Component({
  selector: 'profile-page',
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css']
})
export class ProfilePageComponent implements OnInit {

  constructor(private route: ActivatedRoute, private usersService: UsersService) { }

  id: string;
  private sub: any;

  public genders: typeof GenderType = GenderType;
  public relations: typeof RelationshipType = RelationshipType;

  user: User = new User();

  ngOnInit() {

    this.sub = this.route.params.subscribe(params => {
      this.id = params['id']; // (+) converts string 'id' to a number

      this.getUserInfo(this.id);
    });
  }

  getUserInfo(id: string) {
    this.usersService.getById(id).subscribe(
      user => { this.user = user },
      error => { }
    )
  }

}
