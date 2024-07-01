import { Component,  OnDestroy, OnInit } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription, share } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';
import { Freq, Status } from '../../models/status';

@Component({
  selector: 'app-m1tab',
  templateUrl: './m1tab.component.html',
  styleUrl: './m1tab.component.css',
})
export class M1tabComponent implements OnInit, OnDestroy{

  m1tabform: FormGroup;
  randomNumber: Array<number> = [];
  private currentDataSubscription: Subscription;
  addresses =Addresses;
  status = Status;
  freq = Freq;
  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m1tabform = this.fb.group({
      m1downdata: new FormControl('loopback'),
      m1xm: new FormControl(0),
      m1ym:new FormControl(0) ,
      m1zm:new FormControl(0),
      m1status:new FormControl('CC'),
      //m1selstatus:new FormControl(0),
      //m1counter:new FormControl(0),
      //m1common:new FormControl(0),
      //m1ontime:new FormControl(0),
      //m1linkled:new FormControl(0),
      m1adm:new FormControl('001101'),
      mfreq:new FormControl(1)
    });
    //Listen for changes in the entire form
     this.m1tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m1tab");
    });
  }

  saveFormState(formData: any,whichtab:string): void {
    // Implement the logic to save formData to a file
    // This could be a server call or local storage operation
    //console.warn(this.m1tabform.value);
     this.sharedFormService.updateFormData(formData,whichtab);
  }

  ngOnInit() {

    /**mfreq share between tabs */
    this.currentDataSubscription=this.sharedFormService.currentData.subscribe(data => {
      this.m1tabform.patchValue({
        mfreq: data.mfreq
      }, { emitEvent: false });
    });
    // Subscribe to the blur event of each form control
    // Object.keys(this.m1tabform.controls).forEach(key => {
    //   const control = this.m1tabform.get(key);
    //   if (control) {
    //     control.valueChanges.subscribe(value => {
    //       this.saveFormState(this.m1tabform.value, 'm1tab');
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
