import { GenderType } from "./genders";
import { RelationshipType } from "./relations";

export class User {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  userAbout: string;
  gender: GenderType;
  relationship: RelationshipType;

  constructor(id?: string, userName?: string, firstName?: string, lastName?: string, userAbout?: string, gender?: GenderType, relationship?: RelationshipType) {
    this.id = id;
    this.userName = userName;
    this.firstName = firstName;
    this.lastName = lastName;
    this.userAbout = userAbout;
    this.gender = gender;
    this.relationship = relationship;
  }
}
