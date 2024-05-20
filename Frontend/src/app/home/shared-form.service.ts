// shared-form.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { askmodel } from '../shared/models/ask';

@Injectable({
  providedIn: 'root'
})
export class SharedFormService {
  private formData$ = new BehaviorSubject<askmodel>({
    datamode:'',
    testmode:'',
    couple:0,
    unit:'',
    att:0, 
    pwr:0,
    m1downdata:'',
    m1xm:0,
    m1ym:0,
    m1zm:0,
    m1status:0,
    m1selstatus:0,
    m1counter:0,
    m1common:0,
    m1ontime:0,
    m1linkled:0,
    m1adm:0,
    m1freq:0
  });
  //currentData = this.formData.asObservable();


  get currentData(): Observable<askmodel> { return this.formData$.asObservable(); }

  constructor() {}

  updateFormData(newData: any,whichtab:string) {    
    // Get the current value without subscribing
    let cData=this.formData$.value;    
    switch(whichtab){
      case 'cmd':           
      cData.testmode=newData['testmode']
      cData.datamode=newData['datamode']
      cData.couple=newData['couple']
      cData.unit=newData['unit']
      cData.pwr=newData['pwr']
      cData.att=newData['att']
        break;
      case 'm1tab':
        cData.m1downdata=newData['m1downdata']
        cData.m1xm=newData['m1xm']
        cData.m1ym=newData['m1ym']
        cData.m1zm=newData['m1zm']
        cData.m1status=newData['m1status']
        cData.m1selstatus=newData['m1selstatus']
        cData.m1counter=newData['m1counter']
        cData.m1ontime=newData['m1ontime']
        cData.m1linkled=newData['m1linkled']
        cData.m1adm=newData['m1adm']
        cData.m1freq=newData['m1freq']
        break;
    }
      this.formData$.next(cData);    
  }

  saveFormData() {
    // Logic to save formData to a file
    const currentData = this.formData$.value; // Get the current value without subscribing
     const blob = new Blob([JSON.stringify(this.currentData)], { type: 'application/json' });
          // Use FileSaver or similar library to trigger the download
    // FileSaver.saveAs(blob, 'data.json');
  }
}
