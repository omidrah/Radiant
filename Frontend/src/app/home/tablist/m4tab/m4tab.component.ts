import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';
import { Freq, Status } from '../../models/status';
import { SignalRService } from '../../services/signal-r.service';
import { ReceivePacket } from '../../models/recievePacket';

@Component({
  selector: 'app-m4tab',
  templateUrl: './m4tab.component.html',
  styleUrl: './m4tab.component.css'
})
export class M4tabComponent implements OnInit, OnDestroy{
  m4tabform: FormGroup;
  randomNumber: Array<number> = [];
  private currentDataSubscription: Subscription;
  addresses =Addresses;
  status = Status;
  freq = Freq;
  resPacket: ReceivePacket | null = null;

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService,private signalRService: SignalRService) {
    this.m4tabform = this.fb.group({
      m4downdata: new FormControl('loopback'),
      m4xm: new FormControl(0),
      m4ym:new FormControl(0) ,
      m4zm:new FormControl(0),
      m4status:new FormControl('CC'),
      m4adm:new FormControl('001101'),
      mfreq:new FormControl(1)
    });
     // Listen for changes in the entire form
     this.m4tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m4tab");
    });
  }
  saveFormState(formData: any,whichtab:string): void {
     this.sharedFormService.SendFormData(formData,whichtab);
  }
  ngOnInit() {
  this.currentDataSubscription=this.sharedFormService.currentData.subscribe(data => {
    this.m4tabform.patchValue({
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
