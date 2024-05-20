import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../shared-form.service';


@Component({
  selector: 'app-m1tab',
 
  templateUrl: './m1tab.component.html',
  styleUrl: './m1tab.component.css'
})
export class M1tabComponent implements OnInit, OnDestroy{
  m1tabform: FormGroup;
  subscription: Subscription;
  randomNumber: Array<number> = []; 

  constructor(private fb: FormBuilder , private sharedFormService: SharedFormService) {
    this.m1tabform = this.fb.group({
      m1downdata: new FormControl('loopback'),
      m1xm: new FormControl(0),  
      m1ym:new FormControl(0) ,
      m1zm:new FormControl(0),
      m1status:new FormControl(0),
      m1selstatus:new FormControl(0),
      m1counter:new FormControl(0),
      m1common:new FormControl(0),
      m1ontime:new FormControl(0),
      m1linkled:new FormControl(0),
      m1adm:new FormControl(0),
      m1freq:new FormControl(0)         
    });
     // Listen for changes in the entire form
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
    //setInterval(()=>{this.loadData(); console.log(this.randomNumber) }, 5000);
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
