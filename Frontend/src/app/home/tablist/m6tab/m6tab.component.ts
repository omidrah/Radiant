import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';
import { Freq, Status } from '../../models/status';
import { SignalRService } from '../../services/signal-r.service';
import { ReceivePacket } from '../../models/recievePacket';

@Component({
  selector: 'app-m6tab',
  templateUrl: './m6tab.component.html',
  styleUrl: './m6tab.component.css'
})
export class M6tabComponent implements OnInit, OnDestroy{
  m6tabform: FormGroup;
  randomNumber: Array<number> = [];
  private currentDataSubscription: Subscription;
  addresses =Addresses;
  status = Status;
  freq = Freq;
  resPacket: ReceivePacket | null = null;

  constructor(private fb: FormBuilder ,private signalRService: SignalRService
    , private sharedFormService: SharedFormService) {
    this.m6tabform = this.fb.group({
      m6downdata: new FormControl('loopback'),
      m6xm: new FormControl(0),
      m6ym:new FormControl(0) ,
      m6zm:new FormControl(0),
      m6status:new FormControl('CC'),
      m6adm:new FormControl('001101'),
      mfreq:new FormControl(1)
    });
     // Listen for changes in the entire form
     this.m6tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m6tab");
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
        this.m6tabform.patchValue({
          mfreq: data.sPacket.mfreq
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
