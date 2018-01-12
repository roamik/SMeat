import { Component, OnInit } from "@angular/core";
import { HubConnection } from "@aspnet/signalr-client";
import { User } from "../_models/user";
import { UsersService } from "../_services/users.service";
import { AuthGuard } from '../_guards/auth.guard';
import { environment } from "../../environments/environment";
import { ChatHub } from "../_hubs/chats.hub";
import { BaseTosterService } from "../_services/base-toaster.service";
import { PARAMETERS } from "@angular/core/src/util/decorators";



@Component({
    selector: 'app-chat-page',
    templateUrl: './chat-page.component.html',
    styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit {
    message: any;
    constructor(private usersService: UsersService, private guard: AuthGuard, private tosterService: BaseTosterService) {//private chatHub: ChatHub

    }

    messages: any[] = [];
    private users = new Map<string, User>();

    getUser(user: User): User {
        if (this.users.has(user.id)) {
            return this.users[user.id];
        } else {
            this.users.set(user.id, user);
            return this.users[user.id];
        }
    }

    ngOnInit() {
    //     this.chatHub.started().subscribe(
    //          sucsses => { 
    //              this.onHubConnected();
    //              this.tosterService.info("HUB", "started")
    //          },
    //          error => { this.tosterService.error("HUB", "not started") }
    //     )        
    //     this.chatHub.start();
    }

    // onHubConnected(){
    //     this.chatHub.onSend((connectionId: string, user: User, message: string) => {
    //         this.tosterService.info("HUB", "send");
    //         this.messages.push({
    //             type: 'user', message: message, user: user
    //         });
    //     });

    //     this.chatHub.onConnected((connectionId: string, user: User) => {
    //         this.tosterService.info("HUB", "connected")
    //         this.messages.push({
    //             type: 'server', user: this.getUser(user), message: `user ${user.lastName} connected`
    //         });
    //     });

    //     this.chatHub.onDisconnected((connectionId: string, user: User, error: string) => {
    //         this.tosterService.info("HUB", "disconnected")
    //         this.messages.push({
    //             type: 'server', error: error, user: this.getUser(user), message: `user ${user.lastName} disconnected`
    //         });
    //     });
    // }

    getUserInfo(id: string) {
        this.usersService.getById(id).subscribe(
            user => { this.users.set(id, user) },
            error => { this.tosterService.error() }
        )
    }

    // public sendMessage(): void {
    //     this.chatHub.sendMessage(this.message).subscribe(
    //         sucsses => { this.tosterService.success("Success", "Message send!") },
    //         error => { this.tosterService.error() }
    //     )
    // }
}
