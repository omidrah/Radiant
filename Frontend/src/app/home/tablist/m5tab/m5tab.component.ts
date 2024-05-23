import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../services/shared-form.service';
import { Addresses } from '../../models/address';


@Component({
  selector: 'app-m5tab',
  templateUrl: './m5tab.component.html',
  styleUrl: './m5tab.component.css'
})
export class M5tabComponent implements OnInit, OnDestroy{
  m5tabform: FormGroup;
  randomNumber: Array<number> = [];
  private currentDataSubscription: Subscription;
  addresses =Addresses;

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m5tabform = this.fb.group({
      m5downdata: new FormControl('loopback'),
      m5xm: new FormControl(0),
      m5ym:new FormControl(0) ,
      m5zm:new FormControl(0),
      m5status:new FormControl(0),
      m5selstatus:new FormControl(0),
      m5counter:new FormControl(0),
      m5common:new FormControl(0),
      m5ontime:new FormControl(0),
      m5linkled:new FormControl(0),
      m5adm:new FormControl(0),
      mfreq:new FormControl(0)
    });
     // Listen for changes in the entire form
     this.m5tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m5tab");
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
        this.m5tabform.patchValue({
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
