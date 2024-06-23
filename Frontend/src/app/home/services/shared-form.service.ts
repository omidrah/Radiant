// shared-form.service.ts
import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, Subscription, timer } from 'rxjs';
import { askmodel } from '../models/ask';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class SharedFormService implements  OnDestroy  {
  private timerSubscription: Subscription;

  private formData$ = new BehaviorSubject<askmodel>({
    header:'CMD',
    datamode: 'manual',
    testmode: 'txoff',
    couple: 0,
    //unit: 'w',
    att: 0,
    //pwr: 0,
    /**unique for all tab */
    mfreq: 0,
    rsvd1:0,
    /**m1 */
    m1downdata: 'loopback',
    m1xm: 0,
    m1ym: 0,
    m1zm: 0,
    m1status: 0,
    // m1selstatus: 0,
    // m1counter: 0,
    // m1common: 0,
    // m1ontime: 0,
    // m1linkled: 0,
    m1adm: '001101',
    rsvd2:0,
    /**m2 */
    m2downdata: 'loopback',
    m2xm: 0,
    m2ym: 0,
    m2zm: 0,
    m2status: 0,
    // m2selstatus: 0,
    // m2counter: 0,
    // m2common: 0,
    // m2ontime: 0,
    // m2linkled: 0,
    m2adm: '001101',
    rsvd3:0,
    /**m3 */
    m3downdata: 'loopback',
    m3xm: 0,
    m3ym: 0,
    m3zm: 0,
    m3status: 0,
    // m3selstatus: 0,
    // m3counter: 0,
    // m3common: 0,
    // m3ontime: 0,
    // m3linkled: 0,
    m3adm: '001101',
    rsvd4:0,
    /**m4 */
    m4downdata: 'loopback',
    m4xm: 0,
    m4ym: 0,
    m4zm: 0,
    m4status: 0,
    // m4selstatus: 0,
    // m4counter: 0,
    // m4common: 0,
    // m4ontime: 0,
    // m4linkled: 0,
    m4adm: '001101',
    rsvd5:0,
    /**m5 */
    m5downdata: 'loopback',
    m5xm: 0,
    m5ym: 0,
    m5zm: 0,
    m5status: 0,
    // m5selstatus: 0,
    // m5counter: 0,
    // m5common: 0,
    // m5ontime: 0,
    // m5linkled: 0,
    m5adm: '001101',
    rsvd6:0,
    /**m6 */
    m6downdata: 'loopback',
    m6xm: 0,
    m6ym: 0,
    m6zm: 0,
    m6status: 0,
    // m6selstatus: 0,
    // m6counter: 0,
    // m6common: 0,
    // m6ontime: 0,
    // m6linkled: 0,
    m6adm: '001101',
    checksum:0,
    rsvd7:0,
    footer:'END'
  });
  //currentData = this.formData.asObservable();

  get currentData(): Observable<askmodel> { return this.formData$.asObservable(); }
  constructor(private http: HttpClient) { }

  updateFormData(newData: any, whichtab: string) {
    // Get the current value without subscribing
    let cData = this.formData$.value;
    switch (whichtab) {
      case 'cmd':
        cData.testmode = newData['testmode'];
        cData.datamode = newData['datamode'];
        cData.couple = newData['couple'];
        // cData.unit = newData['unit'];
        // cData.pwr = newData['pwr'];
        cData.att =this.calcPout(newData['att']);
        break;
      case 'm1tab':
        cData.m1downdata = newData['m1downdata']
        cData.m1xm = newData['m1xm']
        cData.m1ym = newData['m1ym']
        cData.m1zm = newData['m1zm']
        cData.m1status = newData['m1status']
        // cData.m1common = newData['m1common']
        // cData.m1selstatus = newData['m1selstatus']
        // cData.m1counter = newData['m1counter']
        // cData.m1ontime = newData['m1ontime']
        // cData.m1linkled = newData['m1linkled']
        cData.m1adm = newData['m1adm']
        cData.mfreq = newData['mfreq']
        break;
      case 'm2tab':
        cData.m2downdata = newData['m2downdata']
        cData.m2xm = newData['m2xm']
        cData.m2ym = newData['m2ym']
        cData.m2zm = newData['m2zm']
        cData.m2status = newData['m2status']
        // cData.m2common = newData['m2common']
        // cData.m2selstatus = newData['m2selstatus']
        // cData.m2counter = newData['m2counter']
        // cData.m2ontime = newData['m2ontime']
        // cData.m2linkled = newData['m2linkled']
        cData.m2adm = newData['m2adm']
        cData.mfreq = newData['mfreq']

        break;
      case 'm3tab':
        cData.m3downdata = newData['m3downdata']
        cData.m3xm = newData['m3xm']
        cData.m3ym = newData['m3ym']
        cData.m3zm = newData['m3zm']
        cData.m3status = newData['m3status']
        // cData.m3common = newData['m3common']
        // cData.m3selstatus = newData['m3selstatus']
        // cData.m3counter = newData['m3counter']
        // cData.m3ontime = newData['m3ontime']
        // cData.m3linkled = newData['m3linkled']
        cData.m3adm = newData['m3adm']
        cData.mfreq = newData['mfreq']

        break;
      case 'm4tab':
        cData.m4downdata = newData['m4downdata']
        cData.m4xm = newData['m4xm']
        cData.m4ym = newData['m4ym']
        cData.m4zm = newData['m4zm']
        cData.m4status = newData['m4status']
        // cData.m4common = newData['m4common']
        // cData.m4selstatus = newData['m4selstatus']
        // cData.m4counter = newData['m4counter']
        // cData.m4ontime = newData['m4ontime']
        // cData.m4linkled = newData['m4linkled']
        cData.m4adm = newData['m4adm']
        cData.mfreq = newData['mfreq']
        break;
      case 'm5tab':
        cData.m5downdata = newData['m5downdata']
        cData.m5xm = newData['m5xm']
        cData.m5ym = newData['m5ym']
        cData.m5zm = newData['m5zm']
        cData.m5status = newData['m5status']
        // cData.m5common = newData['m5common']
        // cData.m5selstatus = newData['m5selstatus']
        // cData.m5counter = newData['m5counter']
        // cData.m5ontime = newData['m5ontime']
        // cData.m5linkled = newData['m5linkled']
        cData.m5adm = newData['m5adm']
        cData.mfreq = newData['mfreq']
        break;
      case 'm6tab':
        cData.m6downdata = newData['m6downdata']
        cData.m6xm = newData['m6xm']
        cData.m6ym = newData['m6ym']
        cData.m6zm = newData['m6zm']
        cData.m6status = newData['m6status']
        // cData.m6common = newData['m6common']
        // cData.m6selstatus = newData['m6selstatus']
        // cData.m6counter = newData['m6counter']
        // cData.m6ontime = newData['m6ontime']
        // cData.m6linkled = newData['m6linkled']
        cData.m6adm = newData['m6adm']
        cData.mfreq = newData['mfreq']
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
    const apiUrl = 'http://localhost:5029'; // Replace with your actual backend API URL
    //this.timerSubscription =timer(0, 10000).subscribe(value => {
     console.log(this.formData$.value); // Emit the value through the Subject
     this.http.post(`${apiUrl}/Home/saveData`, this.formData$.value).subscribe
        (
          (response) => {
            console.log('Data saved successfully!', response);
          },
          (error) => {
            console.error('Error saving data:', error);
          }
        );
    //});
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
