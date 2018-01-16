import { User } from "./user"; 

import { MessageStatus } from "../_enums/message-status";
 
export class Message {
    id: string;
    text: string;
    user: User;
    userId: string;
    status: MessageStatus;    
    dateTime: string;
    chatId: string;
    tempId: string;

    constructor(id?: string, text?: string, userId?: string, user?: User, status?: MessageStatus, dateTime?: string, chatId?: string,  tempId?: string) {
        this.id = id;
        this.tempId = tempId;
        this.text = text;
        this.userId = userId;
        this.user = user;
        this.status = status;        
        this.dateTime = dateTime;
        this.chatId = chatId;
    }
}