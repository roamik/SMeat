import { GenderType } from "../_enums/genders";
import { RelationshipType } from "../_enums/relations";
import { Location } from "../_models/location";
import { WorkPlace } from "./workplace";
import { UserStatusType } from "../_enums/user-status";
import { Random } from "../_helpers/random";

export class User {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  name: string;
  userAbout: string;
  gender: GenderType;
  relationship: RelationshipType;
  locationId: string;
  location: Location;
  workplaceId: string;
  workplace: WorkPlace;
  status: string;
  isFriend: boolean;
  inRequest: boolean;

  constructor(id?: string, userName?: string, firstName?: string, name?: string, lastName?: string, userAbout?: string,
    gender?: GenderType, status?: string, relationship?: RelationshipType,
    location?: Location, locationId?: string, workplaceId?: string, workplace?: WorkPlace, isFriend?: boolean, inRequest?: boolean) {
    this.id = id;
    this.userName = userName;
    this.firstName = firstName;
    this.lastName = lastName;
    this.name = name;
    this.userAbout = userAbout;
    this.gender = gender;
    this.relationship = relationship;
    this.locationId = locationId;
    this.location = location;
    this.workplace = workplace;
    this.workplaceId = workplaceId;
    this.status = status;
    this.isFriend = isFriend;
    this.inRequest = inRequest;
  }
}

