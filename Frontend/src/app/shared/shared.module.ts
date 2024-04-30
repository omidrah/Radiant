import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
 import { AlertModule } from 'ngx-bootstrap/alert';
 import { TabsModule } from 'ngx-bootstrap/tabs';
 import { CollapseModule } from 'ngx-bootstrap/collapse';
 import { ModalModule } from 'ngx-bootstrap/modal';
 import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
 import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
     ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    CollapseModule.forRoot(),
    TabsModule.forRoot(),
    AlertModule.forRoot(),
  ],
  exports:[
    ModalModule,BsDropdownModule,BsDatepickerModule,
    CollapseModule,TabsModule,AlertModule
  ]
})
export class SharedModule { }
