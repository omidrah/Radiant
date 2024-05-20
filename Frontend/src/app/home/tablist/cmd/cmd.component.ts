import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SharedFormService } from '../../shared-form.service';


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

  constructor(private fb: FormBuilder,private sharedFormService: SharedFormService) {
    this.cmdForm = this.fb.group({
      testmode: new FormControl(),
      datamode: new FormControl(),  
      couple:new FormControl('') ,
      unit:new FormControl(''),
      pwr:new FormControl(''),
      att:new FormControl('')         
    });
     // Listen for changes in the entire form
     this.cmdForm.valueChanges.subscribe(values => {
      this.saveFormState(values,"cmd");
    });
  }

  saveFormState(formData: any,whichtab:string): void {
    // Implement the logic to save formData to a file
    // This could be a server call or local storage operation
    //console.warn(this.cmdForm.value);    
    this.sharedFormService.updateFormData(formData,whichtab);
    
  }
  ngOnInit() {
    this.testmode = 'txoff';
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
     this.showDirectMds = false;
      this.showDirectPwr =false;
  }
}
