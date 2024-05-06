import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile } from 'rxjs';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-m1tab',
 
  templateUrl: './m1tab.component.html',
  styleUrl: './m1tab.component.css'
})
export class M1tabComponent implements OnDestroy{
  m1Form: FormGroup;
  showValue = false;
  subscription: Subscription;
  randomNumber: Array<number> = []; //
 constructor(private fb: FormBuilder) {   
  }

 
  ngOnInit() {
    this.m1Form = this.fb.group({
      xt: ["", Validators.required],
      yt: ["", Validators.required],     
      
    });

    //setInterval(()=>{this.loadData(); console.log(this.randomNumber) }, 5000);
   this.subscription = interval(3000)
    //.pipe(takeWhile(() => !stop))
    .subscribe(() => {
      this.loadData(); console.log(this.randomNumber)
    });
   }
  // onSubmit(): void {
  
  // }
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
