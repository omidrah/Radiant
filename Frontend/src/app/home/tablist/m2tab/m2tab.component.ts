import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';
import { Freq, Status } from '../../models/status';
import { ReceivePacket } from '../../models/recievePacket';
import { SignalRService } from '../../services/signal-r.service';

@Component({
  selector: 'app-m2tab',
  templateUrl: './m2tab.component.html',
  styleUrl: './m2tab.component.css'
})
export class M2tabComponent implements OnInit, OnDestroy{
  m2tabform: FormGroup;
  randomNumber: Array<number> = [];
  private currentDataSubscription: Subscription;
  addresses =Addresses;
  status = Status;
  freq = Freq;
  resPacket: ReceivePacket | null = null;

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService,private signalRService: SignalRService) {
    this.m2tabform = this.fb.group({
      m2downdata: new FormControl('loopback'),
      m2xm: new FormControl(0),
      m2ym:new FormControl(0) ,
      m2zm:new FormControl(0),
      m2status:new FormControl('CC'),
      m2adm:new FormControl('001101'),
      mfreq:new FormControl(1)

    });
     // Listen for changes in the entire form
     this.m2tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m2tab");
    });
  }

  saveFormState(formData: any,whichtab:string): void {
    // Implement the logic to save formData to a file
    // This could be a server call or local storage operation
    //console.warn(this.m1tabform.value);
     this.sharedFormService.SendFormData(formData,whichtab);
  }

  ngOnInit() {
    /**mfreq share between tabs */
    this.currentDataSubscription=this.sharedFormService.currentData.subscribe(data => {
      this.m2tabform.patchValue({
        mfreq: data.sPacket.mfreq
      } ,{ emitEvent: false });
    });
    this.signalRService.data$.subscribe(data => { this.resPacket = data; });

   // Subscribe to the blur event of each form control
  //  Object.keys(this.m2tabform.controls).forEach(key => {
  //   const control = this.m2tabform.get(key);
  //   if (control) {
  //     control.valueChanges.subscribe(value => {
  //       this.saveFormState(this.m2tabform.value, 'm2tab');
  //     });
  //   }
  // });
  }

  loadData(){
    this.randomNumber=[];
    for (let index = 0; index < 11; index++) {
      this.randomNumber.push(Math.floor(Math.random() * (1000 - 2 + 1) + 2));
    }
  }

  ngOnDestroy() {
    this.currentDataSubscription.unsubscribe();

  }

}
