import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../shared-form.service';

@Component({
  selector: 'app-m2tab',  
  templateUrl: './m2tab.component.html',
  styleUrl: './m2tab.component.css'
})
export class M2tabComponent implements OnInit, OnDestroy{
  m2tabform: FormGroup;
  subscription: Subscription;
  randomNumber: Array<number> = []; 

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m2tabform = this.fb.group({
      m2downdata: new FormControl('loopback'),
      m2xm: new FormControl(0),  
      m2ym:new FormControl(0) ,
      m2zm:new FormControl(0),
      m2status:new FormControl(0),
      m2selstatus:new FormControl(0),
      m2counter:new FormControl(0),
      m2common:new FormControl(0),
      m2ontime:new FormControl(0),
      m2linkled:new FormControl(0),
      m2adm:new FormControl(0),
      m2freq:new FormControl(0)         
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
    this.subscription = interval(10000)
     //.pipe(takeWhile(() => !stop))
     .subscribe(() => {
      //this.loadData(); console.log(this.randomNumber)}
      });
      
  }

  loadData(){
    this.randomNumber=[];
    for (let index = 0; index < 11; index++) {
      this.randomNumber.push(Math.floor(Math.random() * (1000 - 2 + 1) + 2));
    } 
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();    
  }

}
