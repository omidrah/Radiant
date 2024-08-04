import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { ReceivePacket,ReP } from '../models/recievePacket';

@Injectable({
  providedIn: 'root'
})

export class SignalRService {
  private dataSource = new BehaviorSubject<ReP | null>(null);
  data$ = this.dataSource.asObservable();
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.startConnection();
    this.addTransferHubListener();
  }

  private startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/dataHub')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection started'))
      .catch(err => console.log('Error while starting connection:' + err));
  }

  private addTransferHubListener() {
    this.hubConnection.on('ReceiveData', (data: ReP) => {
      console.log('Received data from SignalR:', data); // Log the received data
      this.dataSource.next(data);
    });
  }
}
