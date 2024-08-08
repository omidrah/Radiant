import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { ReceivePacket } from '../models/recievePacket';
import { Constants } from '../../config/constants';

@Injectable({
  providedIn: 'root'
})

export class SignalRService {
  private dataSource = new BehaviorSubject<ReceivePacket | null>(null);
  data$ = this.dataSource.asObservable();
  private hubConnection: signalR.HubConnection;
  apiUrl = Constants.API_ENDPOINT;
  constructor() {
    this.startConnection();
    this.addTransferHubListener();
  }

  private startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/dataHub}`)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connection started'))
      .catch(err => console.log('Error while starting connection:' + err));
  }

  private addTransferHubListener() {
    this.hubConnection.on('ReceiveData', (data:ReceivePacket) => {
     // console.log('Received data from SignalR:', data); // Log the received data
      this.dataSource.next(data);
    });
  }
}
