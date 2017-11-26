export class User {
  id: string;
  userName: string;
  name: string;
  firstName: string;
  lastName: string;

  constructor(id?: string, userName?: string, name?: string, firstName?: string, lastName?: string) {
    this.id = id;
    this.userName = userName;
    this.name = name;
    this.firstName = firstName;
    this.lastName = lastName;
  }
}
