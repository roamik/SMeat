import { User } from "./user";

export class Friend {

  friend: User;

  constructor(friend?: User) {
    this.friend = friend;
  }
}
