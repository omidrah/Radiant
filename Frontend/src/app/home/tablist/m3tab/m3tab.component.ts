import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';
import { Freq, Status } from '../../models/status';
import { ReceivePacket } from '../../models/recievePacket';
import { SignalRService } from '../../services/signal-r.service';

@Component({
  selector: 'app-m3tab',
  templateUrl: './m3tab.component.html',
  styleUrl: './m3tab.component.css'
})
export class M3tabComponent implements OnInit, OnDestroy{
  m3tabform: FormGroup;
  randomNumber: Array<number> = [];
  private currentDataSubscription: Subscription;
  addresses =Addresses;
  status = Status;
  freq = Freq;
  resPacket: ReceivePacket | null = null;

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService,private signalRService: SignalRService) {
    this.m3tabform = this.fb.group({
      m3downdata: new FormControl('loopback'),
      m3xm: new FormControl(0),
      m3ym:new FormControl(0) ,
      m3zm:new FormControl(0),
      m3status:new FormControl('CC'),
      m3adm:new FormControl('001101'),
      mfreq:new FormControl(1)
    });
     this.m3tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m3tab");
    });
  }
  saveFormState(formData: any,whichtab:string): void {
     this.sharedFormService.SendFormData(formData,whichtab);
  }
  ngOnInit() {
  this.currentDataSubscription=this.sharedFormService.currentData.subscribe(data => {
    this.m3tabform.patchValue({
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
