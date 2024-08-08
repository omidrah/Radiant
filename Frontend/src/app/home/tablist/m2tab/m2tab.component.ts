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

     this.sharedFormService.SendFormData(formData,whichtab);
  }

  ngOnInit() {
    /**mfreq share between tabs */
    this.currentDataSubscription=this.sharedFormService.currentData.subscribe(data => {
      this.m2tabform.patchValue({
        mfreq: data.mfreq
      } ,{ emitEvent: false });
    });
    this.signalRService.data$.subscribe(data => { this.resPacket = data; });
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
