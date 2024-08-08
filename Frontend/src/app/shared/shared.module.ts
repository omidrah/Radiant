import { ModuleWithProviders, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms'; // Import FormsModule

import { CommonModule } from '@angular/common';
import { DigiclockComponent } from './digiclock/digiclock.component';
import { AlertModule } from 'ngx-bootstrap/alert';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { UtilService } from './util/UtilService';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxSliderModule } from '@angular-slider/ngx-slider';

@NgModule({
  declarations: [
    DigiclockComponent
  ],
  imports: [
    CommonModule,
    FormsModule, // Add FormsModule here
    ModalModule.forRoot(),
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    CollapseModule.forRoot(),
    TabsModule.forRoot(),
    AlertModule.forRoot(),
    NgxSliderModule,
    NgSelectModule
  ],
  exports:[
    ModalModule,
    BsDropdownModule,
    BsDatepickerModule,
    CollapseModule,
    TabsModule,
    NgSelectModule,
    AlertModule,
    NgxSliderModule,
    DigiclockComponent,
  ],
  providers:[UtilService]
})
export class SharedModule {

  static forRoot() {
    // Forcing the whole app to use the returned providers from the roorModule only.
    return {
      ngModule: SharedModule,
      providers: [ /* All of your services here. It will hold the services needed by `itself`. */]
    };
  }
}
