import { Message } from "./message";
import { User } from "./user";

export class Chat {
    id: string;
    name: string;
    messages: Message[];
    user: User;    
    userId: string;
    dateTime: string;
    constructor(id?: string, name?: string, messages?: Message[], user?: User, userId?: string, dateTime?: string) {
        this.id= id;
        this.name=name;
        this.messages = messages;
        this.user = user;
        this.userId = userId;
        this.dateTime = dateTime;        
    }
}