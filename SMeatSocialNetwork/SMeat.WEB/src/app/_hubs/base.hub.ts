import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { environment } from "../../environments/environment";
import { AuthGuard } from "../_guards/auth.guard";
import { Inject } from "@angular/core/core";
import { HubConnection } from "@aspnet/signalr-client";
import { IHubConnectionOptions } from "@aspnet/signalr-client/dist/src/IHubConnectionOptions";

export enum ConnectionState {
  Connecting = 1,
  Connected = 2,
  Reconnecting = 3,
  Disconnected = 4
}

export class BaseHub {
  protected BASEURL: string;

  protected starting: Observable<any>;
  protected connectionState: Observable<ConnectionState>;
  protected error: Observable<string>;
  
  protected connectionStateSubject = new Subject<ConnectionState>();
  protected startingSubject = new Subject<any>();
  protected errorSubject = new Subject<any>();

  protected hubConnection: any;
  protected hubProxy: any;

  constructor(protected readonly hubName: string, protected guard: AuthGuard) {
    this.BASEURL = environment.baseApi;
    // if ((window as any).$ === undefined || (window as any).$.hubConnection === undefined) {
    //   throw new Error(`The variable '$' or the .hubConnection() function are not defined.`);
    // }

    this.connectionState = this.connectionStateSubject.asObservable();
    this.error = this.errorSubject.asObservable();
    this.starting = this.startingSubject.asObservable();
    
    // this.hubConnection = (window as any).$.hubConnection();
    // this.hubConnection.logging = true;
    // this.hubConnection.url =( this.BASEURL+ "signalr");//+ "chat?token=" + this.guard.token;;
    // this.hubConnection.qs = {'token': this.guard.token};
    
    // this.hubProxy = this.hubConnection.createHubProxy(hubName);


    var hubUrl: string = this.BASEURL + hubName + "?token=" + this.guard.token;
    this.hubConnection = new HubConnection(hubUrl);
    // this.hubConnection.stateChanged((state: any) => {
    //   let newState = ConnectionState.Connecting;
    //   switch (state.newState) {
    //     case (window as any).$.signalR.connectionState.connecting:
    //       newState = ConnectionState.Connecting;
    //       break;
    //     case (window as any).$.signalR.connectionState.connected:
    //       newState = ConnectionState.Connected;
    //       break;
    //     case (window as any).$.signalR.connectionState.reconnecting:
    //       newState = ConnectionState.Reconnecting;
    //       break;
    //     case (window as any).$.signalR.connectionState.disconnected:
    //       newState = ConnectionState.Disconnected;
    //       break;
    //   }

    //   this.connectionStateSubject.next(newState);
    //});

    // this.hubConnection.disconnected(() => {
    //   setTimeout(function() {
    //     this.hubConnection.start();
    //   }, 5000); // Restart connection after 5 seconds.
    // });

    // this.hubConnection.error((error: any) => {
    //   this.errorSubject.next(error);
    // });
  }

  start(): void {
    this.hubConnection.start()
      .then(() => {
        this.startingSubject.next();
      })
      .catch((error: any) => {
        this.startingSubject.error(error);
      });
  }
}
