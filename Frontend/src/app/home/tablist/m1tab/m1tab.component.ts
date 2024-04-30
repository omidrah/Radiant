import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-m1tab',
 
  templateUrl: './m1tab.component.html',
  styleUrl: './m1tab.component.css'
})
export class M1tabComponent {
  m1Form: FormGroup;
  showValue = false;
  constructor(private fb: FormBuilder) {}
  ngOnInit() {
    this.m1Form = this.fb.group({
      xt: ["", Validators.required],
      yt: ["", Validators.required],     
      
    });
  }
  onSubmit(): void {
  
  }
}
