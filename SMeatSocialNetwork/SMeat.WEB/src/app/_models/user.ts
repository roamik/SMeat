export class User {
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  userAbout: string;

  constructor(id?: string, userName?: string, firstName?: string, lastName?: string, userAbout?: string,) {
    this.id = id;
    this.userName = userName;
    this.firstName = firstName;
    this.lastName = lastName;
    this.userAbout = userAbout;
  }
}
