import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { ReceivePacket } from '../models/recievePacket';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  private dataSource = new BehaviorSubject<ReceivePacket | null>(null);
  data$ = this.dataSource.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/dataHub')
      .build();

    this.hubConnection.on('ReceiveData', (data: ReceivePacket) => {
      this.dataSource.next(data);
    });

    this.hubConnection.start().catch(err => console.error(err));
   }
}
