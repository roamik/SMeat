import { Component, OnInit } from "@angular/core";
import { HubConnection } from "@aspnet/signalr-client";
import { User } from "../_models/user";
import { UsersService } from "../_services/users.service";
import { AuthGuard } from '../_guards/auth.guard';
import { environment } from "../../environments/environment";



//const BASEURL = "https://smeat-web-api.herokuapp.com/";
//const BASEURL = "http://localhost:27121/";

@Component({
    selector: 'app-chat-page',
    templateUrl: './chat-page.component.html',
    styleUrls: ['./chat-page.component.css']
})
export class ChatPageComponent implements OnInit {
    readonly BASEURL: string;
    message: any;
    constructor(private usersService: UsersService, private guard: AuthGuard ) {   
        this.BASEURL = environment.baseApi;
    }
    
    private hubConnection: HubConnection;
    messages: any[] = [];
    private users = new Map<string, User>();

    getUser(user: User): User {
        if (this.users.has(user.id)) {
            return this.users[user.id]
        } else {
            this.users.set(user.id, user);
            return this.users[user.id];
        }       
    }

    ngOnInit() {
        var hubUrl: string = this.BASEURL + "chat?token=" + this.guard.token;
        this.hubConnection = new HubConnection(hubUrl);

        this.hubConnection.start()
            .then(() => console.log('Connection started!'))
            .catch(err => console.log('Error while establishing connection :('));

        this.hubConnection.on('OnSend', (connectionId: string, user: User, message: string) => {
            console.log({
                type: 'user', message: message, user: user
            });
            this.messages.push({
                type: 'user', message: message, user: user
            });
        });

        this.hubConnection.on('OnConnected', (connectionId: string, user: User) => {
            this.messages.push({
                type: 'server', user: this.getUser(user), message: `user ${user.lastName} connected`
            });
        });

        this.hubConnection.on('OnDisconnected', (connectionId: string, user: User, error: string) => {
            this.messages.push({
                type: 'server', error: error, user: this.getUser(user), message: `user ${user.lastName} disconnected`
            });
        });
    }

    getUserInfo(id: string) {
        this.usersService.getById(id).subscribe(
            user => { this.users.set(id, user)},
            error => { }
        )
    }

    public sendMessage(message): void {
        this.hubConnection
            .invoke('SendAsync', this.message)
            .catch(err => console.error(err));
    }
}
