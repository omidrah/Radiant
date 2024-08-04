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
      .withUrl('http://localhost:5000/dataHub') // Ensure the URL is correct and accessible
      .build();

  this.hubConnection.start().then(() => {
      console.log('SignalR Connected'); // Confirm successful connection
    }).catch(err => {
      console.error('SignalR Connection Error:', err); // Log connection errors
    });
    
    this.hubConnection.on('ReceiveData', (data: ReceivePacket) => {
      console.log('Received data from SignalR:', data); // Add a console log to verify data reception
      this.dataSource.next(data);
    });

  
  }
}
