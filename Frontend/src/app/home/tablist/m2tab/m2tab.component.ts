import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';

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
  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m2tabform = this.fb.group({
      m2downdata: new FormControl('loopback'),
      m2xm: new FormControl(0),
      m2ym:new FormControl(0) ,
      m2zm:new FormControl(0),
      m2status:new FormControl(0),
      //m2selstatus:new FormControl(0),
      //m2counter:new FormControl(0),
      //m2common:new FormControl(0),
     // m2ontime:new FormControl(0),
      //m2linkled:new FormControl(0),
      m2adm:new FormControl(0),
      mfreq:new FormControl(0)
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
     this.sharedFormService.updateFormData(formData,whichtab);
  }

  ngOnInit() {
    /**mfreq share between tabs */
    this.currentDataSubscription=this.sharedFormService.currentData.subscribe(data => {
      this.m2tabform.patchValue({
        mfreq: data.mfreq
      } ,{ emitEvent: false });
    });
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
