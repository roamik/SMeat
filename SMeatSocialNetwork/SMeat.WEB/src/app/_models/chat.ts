import { Message } from "./message";
import { User } from "./user";
import * as _ from "lodash";
import { UserStatusType } from "../_enums/user-status";

export class Chat {
    id: string;
    text: string;
    messages: Message[];
    userChats: UserChat[];  
    userId: string;
    user:User;   
    dateTime: string;
    get lastMessageDate() : string {
        return _.last(this.messages).dateTime;
    }
    get pictureUrl():string{
        return "http://emilcarlsson.se/assets/harveyspecter.png";
    }
    constructor(id?: string, text?: string, messages?: Message[], user?: User, userId?: string, dateTime?: string, userChats?:UserChat[]) {
        this.id= id;
        this.text=text;
        this.messages = messages;
        this.userChats = userChats;
        this.user = user;
        this.userId = userId;
        this.dateTime = dateTime;       
    }
}

export class UserChat {
    user: User;  
    userId: string;
    chat: Chat;  
    chatId: string;    
    status: UserStatusType;
    constructor(user?: User, userId?: string, chat?: Chat, chatId?: string, status?: UserStatusType){        
        this.user = user;
        this.userId = userId;
        this.chat = chat;
        this.chatId = chatId;
        this.status = status;
    }
}
