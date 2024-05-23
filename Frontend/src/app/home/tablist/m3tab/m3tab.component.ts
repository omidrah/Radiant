import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';

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

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m3tabform = this.fb.group({
      m3downdata: new FormControl('loopback'),
      m3xm: new FormControl(0),
      m3ym:new FormControl(0) ,
      m3zm:new FormControl(0),
      m3status:new FormControl(0),
      m3selstatus:new FormControl(0),
      m3counter:new FormControl(0),
      m3common:new FormControl(0),
      m3ontime:new FormControl(0),
      m3linkled:new FormControl(0),
      m3adm:new FormControl(0),
      mfreq:new FormControl(0)
    });
     // Listen for changes in the entire form
     this.m3tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m3tab");
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
    this.m3tabform.patchValue({
      mfreq: data.mfreq
    } ,{ emitEvent: false });
  });
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
