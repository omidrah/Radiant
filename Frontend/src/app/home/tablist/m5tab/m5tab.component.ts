import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { interval, takeWhile,Subscription } from 'rxjs';
import { SharedFormService } from '../../shared-form.service';


@Component({
  selector: 'app-m5tab', 
  templateUrl: './m5tab.component.html',
  styleUrl: './m5tab.component.css'
})
export class M5tabComponent implements OnInit, OnDestroy{
  m5tabform: FormGroup;
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
