import { RequestStatus } from "../_enums/requestStatus";
import { User } from "./user";

export class Request {

  user: User;
  friend: User;
  status: RequestStatus;

  constructor(user?: User, friend?: User, status?: RequestStatus) {
    this.user = user;
    this.friend = friend;
    this.status = status;
  }
}
