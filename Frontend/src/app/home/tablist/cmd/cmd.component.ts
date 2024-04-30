import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-cmd', 
  templateUrl: './cmd.component.html',
  styleUrl: './cmd.component.css'
})
export class CmdComponent implements OnInit {
  cmdForm: FormGroup;
  showDirectMds= false;
  showDirectPwr=false;
  testmode:string='txoff';
  datamode:string='manual';
  unit:string='w';
  showValue = false;

  constructor(private fb: FormBuilder) {
    this.cmdForm = this.fb.group({
      testmode: new FormControl(),
      datamode: new FormControl(),  
      couple:new FormControl('')   ,
      unit:new FormControl('')   ,
      pwr:new FormControl('')   ,
      att:new FormControl('')   ,

      
    });
  }
  ngOnInit() {
    this.testmode = 'txoff';
  }
  onSubmit(): void {
    
    this.showValue = !this.showValue;
    console.warn(this.cmdForm.value);
    //console.log(this.showValue)
  }
  

   testmodeChanged($event){
    let radioValue = event.target['value'];
    if(radioValue =='txoff'){
      this.showDirectMds = false;
      this.showDirectPwr =false;
    }
    else if(radioValue =='directmds'){
       this.showDirectMds = true;
       this.showDirectPwr =false;
     }else{ //directpwr
       this.showDirectMds = false;
       this.showDirectPwr =true;
     }
     
  }

  datamodeChanged($event){
    let radioValue = event.target['value'];
     console.log(radioValue);
  }
}
