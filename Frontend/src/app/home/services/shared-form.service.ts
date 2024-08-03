// shared-form.service.ts
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, Subscription, timer } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Packet, PacketImpl } from '../models/packet';
import { SendPacket } from '../models/sendPacket';
import { ReceivePacket } from '../models/recievePacket';
@Injectable({
  providedIn: 'root'
})
export class SharedFormService implements  OnDestroy  {
  private timerSubscription: Subscription;

   sendPacket = new SendPacket(
    'CMD',
    'txof',
    //couple
     0,
    // att:
     0,
    //mfreq:
     1,
    //selftest:
    1,
    //m1xm:
     0,
    //m1ym:
    0,
    //m1zm:
     0,
    //m1status:
    'CC',
    //m1adm:
    '001101',
    //datamode1:
    'loopback',
    //crc:
    3,
    //m2xm:
    0,
    //m2ym:
    0,
    //m2zm:
    0,
    //m2status:
    'CC',
    //m2adm:
    '001101',
    //datamode2:
    'loopback',
    //rsvd3:
    0,
    //m3xm:
     0,
    //m3ym:
     0,
    //m3zm:
     0,
    //m3status:
    'CC',
    //m3adm:
     '001101',
    //datamode3:
    'loopback',
    //rsvd4:
    0,
    //m4xm:
     0,
    //m4ym:
     0,
    //m4zm:
    0,
    //m4status:
    'CC',
    //m4adm:
    '001101',
    //datamode4:
    'loopback',
    //rsvd5:
    0,
    //m5xm:
    0,
    //m5ym:
    0,
    //m5zm:
    0,
    //m5status:
    'CC',
    //m5adm:
    '001101',
    //datamode5:
    'loopback',
    //rsvd6:
    0,
    //m6xm:
     0,
    //m6ym:
     0,
    //m6zm:
     0,
    //m6status:
     'CC',
    //m6adm:
    '001101',
    //datamode6:
    'loopback',
    //rsvd7:
    0,
    //checksum:
    0,
    //rsvd8:
    0,
    //footer:
    'END'
  );
// Create an instance of ReceivePacket
 receivePacket = new ReceivePacket(
  "RCUV", 31, 32, 33, 34,  35,  36,  37,  38,  39,  40,  41,  42,
  43,  44,  45,  46,  47,  48,  49,  50,  51,  52,  53,  54,  55,
  56,  57,  58,  59,  60,  61,  62,  63,  64,  65,  66,  67,  68,
  69,  70,  71,  72,  73,  74,  75,  76,  77,  78,  79,  80,  81,
  82,  83,  84,  85,  86,  87,  88,  89,  90,  91,  92,  93,  94,
  95,  96,  97,  98,  99,  100,  101,  102,  103,  104,  105,  106,
  107,  108,  109,  110,  111,  112,  113,  114,  115,  116,  "PLIN"
);

// Create an instance of PacketImpl
 packet = new PacketImpl(this.sendPacket, this.receivePacket);
  private formData$ = new BehaviorSubject<Packet>(this.packet);
  get currentData(): Observable<Packet> { return this.formData$.asObservable(); }
  constructor(private http: HttpClient) { }

  SendFormData(newData: any, whichtab: string) {
    // Get the current value without subscribing
    let cData = this.formData$.value;
    switch (whichtab) {
      case 'cmd':
        cData.sPacket.testmode = newData['testmode'];
        cData.sPacket.couple = newData['couple'];
        cData.sPacket.att =this.calcPout(newData['att']);
        cData.sPacket.selftest = newData['selftest'];
        cData.sPacket.crc = newData['crc'];
        break;
      case 'm1tab':
        cData.sPacket.datamode1 = newData['m1downdata']
        cData.sPacket.m1xm = newData['m1xm']
        cData.sPacket.m1ym = newData['m1ym']
        cData.sPacket.m1zm = newData['m1zm']
        cData.sPacket.m1status = newData['m1status']
        cData.sPacket.m1adm = newData['m1adm']
        cData.sPacket.mfreq = newData['mfreq']
        break;
      case 'm2tab':
        cData.sPacket.datamode2 = newData['m2downdata']
        cData.sPacket.m2xm = newData['m2xm']
        cData.sPacket.m2ym = newData['m2ym']
        cData.sPacket.m2zm = newData['m2zm']
        cData.sPacket.m2status = newData['m2status']
        cData.sPacket.m2adm = newData['m2adm']
        cData.sPacket.mfreq = newData['mfreq']
        break;
      case 'm3tab':
        cData.sPacket.datamode3 = newData['m3downdata']
        cData.sPacket.m3xm = newData['m3xm']
        cData.sPacket.m3ym = newData['m3ym']
        cData.sPacket.m3zm = newData['m3zm']
        cData.sPacket.m3status = newData['m3status']
        cData.sPacket.m3adm = newData['m3adm']
        cData.sPacket.mfreq = newData['mfreq']
        break;
      case 'm4tab':
        cData.sPacket.datamode4 = newData['m4downdata']
        cData.sPacket.m4xm = newData['m4xm']
        cData.sPacket.m4ym = newData['m4ym']
        cData.sPacket.m4zm = newData['m4zm']
        cData.sPacket.m4status = newData['m4status']
        cData.sPacket.m4adm = newData['m4adm']
        cData.sPacket.mfreq = newData['mfreq']
        break;
      case 'm5tab':
        cData.sPacket.datamode5 = newData['m5downdata']
        cData.sPacket.m5xm = newData['m5xm']
        cData.sPacket.m5ym = newData['m5ym']
        cData.sPacket.m5zm = newData['m5zm']
        cData.sPacket.m5status = newData['m5status']
        cData.sPacket.m5adm = newData['m5adm']
        cData.sPacket.mfreq = newData['mfreq']
        break;
      case 'm6tab':
        cData.sPacket.datamode6 = newData['m6downdata']
        cData.sPacket.m6xm = newData['m6xm']
        cData.sPacket.m6ym = newData['m6ym']
        cData.sPacket.m6zm = newData['m6zm']
        cData.sPacket.m6status = newData['m6status']
        cData.sPacket.m6adm = newData['m6adm']
        cData.sPacket.mfreq = newData['mfreq']
        break;
    }
    this.formData$.next(cData);
  }
  calcPout(pout:number){
    //1403-02-31 . mr.Nader said for calculate pout..
    // 10->0   and -90->200
     return Math.abs((pout -10)*2);
  }
/**start timer every 1 seconds read values and send to dotnetbackend */
  startTimer() {
    const apiUrl = 'http://localhost:5000'; // Replace with your actual backend API URL
    this.timerSubscription =timer(0, 1000).pipe().subscribe(value => {
     console.log(this.formData$.value.sPacket); // Emit the value through the Subject
     this.http.post(`${apiUrl}/Home/sendData`, this.formData$.value.sPacket).subscribe
        (
          (response) => {
            console.log('Data saved successfully!', response);
          },
          (error) => {
            console.error('Error saving data:', error);
          }
        );
    });
  }
  startTimer_nodeJsbackend() {
    const apiUrl = 'http://localhost:3000'; // Replace with your actual backend API URL
    this.timerSubscription =timer(0, 10000).subscribe(value => {
     //console.log(this.formData$.value); // Emit the value through the Subject
     this.http.post(`${apiUrl}/api/save-data`, this.formData$.value).subscribe
        (
          (response) => {
            console.log('Data saved successfully!', response);
          },
          (error) => {
            console.error('Error saving data:', error);
          }
        );
    });
  }
  saveFormData() {
    // Logic to save formData to a file
    const currentData = this.formData$.value; // Get the current value without subscribing
    const blob = new Blob([JSON.stringify(this.currentData)], { type: 'application/json' });
    // Use FileSaver or similar library to trigger the download
    // FileSaver.saveAs(blob, 'data.json');
  }
  ngOnDestroy() {
    this.timerSubscription.unsubscribe();
  }
}
