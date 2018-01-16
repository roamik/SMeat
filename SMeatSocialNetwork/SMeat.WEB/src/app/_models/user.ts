import { GenderType } from "../_enums/genders";
import { RelationshipType } from "../_enums/relations";
import { Location } from "../_models/location";
import { WorkPlace } from "./workplace";

export class User {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  userAbout: string;
  gender: GenderType;
  relationship: RelationshipType;
  locationId: string;
  location: Location;
  workplaceId: string;
  workplace: WorkPlace;

  constructor(id?: string, userName?: string, firstName?: string, lastName?: string, userAbout?: string, gender?: GenderType, relationship?: RelationshipType, location?: Location, locationId?: string, workplaceId?: string, workplace?: WorkPlace) {
    this.id = id;
    this.userName = userName;
    this.firstName = firstName;
    this.lastName = lastName;
    this.userAbout = userAbout;
    this.gender = gender;
    this.relationship = relationship;
    this.locationId = locationId;
    this.location = location;
    this.workplace = workplace;
    this.workplaceId = workplaceId;
  }
}

