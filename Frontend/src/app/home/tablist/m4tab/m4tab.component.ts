import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../shared-form.service';

@Component({
  selector: 'app-m4tab', 
  templateUrl: './m4tab.component.html',
  styleUrl: './m4tab.component.css'
})
export class M4tabComponent implements OnInit, OnDestroy{
  m4tabform: FormGroup;
  randomNumber: Array<number> = []; 
  private currentDataSubscription: Subscription;

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m4tabform = this.fb.group({
      m4downdata: new FormControl('loopback'),
      m4xm: new FormControl(0),  
      m4ym:new FormControl(0) ,
      m4zm:new FormControl(0),
      m4status:new FormControl(0),
      m4selstatus:new FormControl(0),
      m4counter:new FormControl(0),
      m4common:new FormControl(0),
      m4ontime:new FormControl(0),
      m4linkled:new FormControl(0),
      m4adm:new FormControl(0),
      mfreq:new FormControl(0)         
    });
     // Listen for changes in the entire form
     this.m4tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m4tab");
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
    this.m4tabform.patchValue({
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
