import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../shared-form.service';

@Component({
  selector: 'app-m6tab',  
  templateUrl: './m6tab.component.html',
  styleUrl: './m6tab.component.css'
})
export class M6tabComponent implements OnInit, OnDestroy{
  m6tabform: FormGroup;
  subscription: Subscription;
  randomNumber: Array<number> = []; 

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m6tabform = this.fb.group({
      m6downdata: new FormControl('loopback'),
      m6xm: new FormControl(0),  
      m6ym:new FormControl(0) ,
      m6zm:new FormControl(0),
      m6status:new FormControl(0),
      m6selstatus:new FormControl(0),
      m6counter:new FormControl(0),
      m6common:new FormControl(0),
      m6ontime:new FormControl(0),
      m6linkled:new FormControl(0),
      m6adm:new FormControl(0),
      m6freq:new FormControl(0)         
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
     this.sharedFormService.updateFormData(formData,whichtab);
  }

  ngOnInit() {  
    this.subscription = interval(10000)
     //.pipe(takeWhile(() => !stop))
     .subscribe(() => {
      //this.loadData(); console.log(this.randomNumber)}
      console.log(this.sharedFormService.currentData);
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
