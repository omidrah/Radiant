import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SharedFormService } from '../../services/shared-form.service';
@Component({
  selector: 'app-cmd',
  templateUrl: './cmd.component.html',
  styleUrl: './cmd.component.css'
})
export class CmdComponent implements OnInit {
  cmdForm: FormGroup;
  showDirectMds= false;
  showDirectPwr=false;
  constructor(private fb: FormBuilder,private sharedFormService: SharedFormService) {}
  saveFormState(formData: any,whichtab:string): void {
    // Implement the logic to save formData to a file
    // This could be a server call or local storage operation
    //console.warn(this.cmdForm.value);
    this.sharedFormService.updateFormData(formData,whichtab);
  }
  ngOnInit() {
    this.cmdForm = this.fb.group({
      testmode: new FormControl('txoff'),
      // datamode: new FormControl('manual'),
      couple:new FormControl(0) ,
      unit:new FormControl('w'),
      pwr:new FormControl(0),
      att:new FormControl(0),
      crc:new FormControl(true),
      SelfTest:new FormControl(true)
    });
     // Listen for changes in the entire form
     this.cmdForm.valueChanges.subscribe(values => {
      this.saveFormState(values,"cmd");
    });
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
     //console.log(radioValue);
     //this.showDirectMds = false;
      //this.showDirectPwr =false;
  }
 //return unit radio selected
  get unit(): any {
    return this.cmdForm.get('unit').value;
  }
  get testmode(): any {
    return this.cmdForm.get('testmode').value;
  }
  get datamode(): any {
    return this.cmdForm.get('datamode').value;
  }
  get couple(): number {
    return this.cmdForm.get('couple').value;
  }
  get pwr(): number {
    return this.cmdForm.get('pwr').value;
  }
  get att(): number {
    return this.cmdForm.get('att').value;
  }
  get crc(): number {
    return this.cmdForm.get('crc').value;
  }
  get selftest(): number {
    return this.cmdForm.get('selftest').value;
  }
}
