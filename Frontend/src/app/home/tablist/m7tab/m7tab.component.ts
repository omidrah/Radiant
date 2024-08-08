import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { SignalRService } from '../../services/signal-r.service';
import { SharedFormService } from '../../services/shared-form.service';

@Component({
  selector: 'app-m7tab',
  templateUrl: './m7tab.component.html',
  styleUrl: './m7tab.component.css'
})
export class M7tabComponent {
  m7tabform: FormGroup;

  constructor(private fb: FormBuilder ,private signalRService: SignalRService, private sharedFormService: SharedFormService) {
    this.m7tabform = this.fb.group({
      cfar_coef: new FormControl(0),
      downlink_att:new FormControl(0) ,
      uplink_gain:new FormControl(0),
      self_tester_att:new FormControl(0)
    });
     this.m7tabform.valueChanges.subscribe(values => {
      this.saveFormState(values,"m7tab");
    });
  }
  saveFormState(formData: any,whichtab:string): void {
    this.sharedFormService.SendFormData(formData,whichtab);
 }
}
