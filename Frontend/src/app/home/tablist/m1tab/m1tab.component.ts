import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription, share } from 'rxjs';
import { SharedFormService } from '../../shared-form.service';

@Component({
  selector: 'app-m1tab', 
  templateUrl: './m1tab.component.html',
  styleUrl: './m1tab.component.css'
})
export class M1tabComponent implements OnInit, OnDestroy{
  m1tabform: FormGroup;
  randomNumber: Array<number> = []; 
  private currentDataSubscription: Subscription;
  addresses = [
    { id: 1, name: '001101' },
    { id: 2, name: '001110' },
    { id: 3, name: '010101' },
    { id: 4, name: '011100' },
    { id: 5, name: '010110' },
    { id: 6, name: '011001' },
    { id: 7, name: '011010' },
    { id: 8, name: '101001' },
    { id: 9, name: '011101' },
    { id: 10, name: '100101' },
    { id: 11, name: '100110' },
    { id: 12, name: '110011' },
    { id: 13, name: '011011' },
    { id: 14, name: '100111' },
    { id: 15, name: '101010' },
    { id: 16, name: '101106' },
    { id: 17, name: '010011' },
    { id: 18, name: '101100' },
    { id: 19, name: '110010' },
    { id: 20, name: '110101' },
    { id: 21, name: '101011' },
    { id: 22, name: '101110' },
    { id: 23, name: '110110' },
    { id: 24, name: '111010' }        
];

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
      mfreq:new FormControl(0)         
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
