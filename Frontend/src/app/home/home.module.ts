import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { CmdComponent } from './tablist/cmd/cmd.component';
import { TablistComponent } from './tablist/tablist.component';
import { M1tabComponent } from './tablist/m1tab/m1tab.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { M2tabComponent } from './tablist/m2tab/m2tab.component';
import { M3tabComponent } from './tablist/m3tab/m3tab.component';
import { M4tabComponent } from './tablist/m4tab/m4tab.component';
import { M5tabComponent } from './tablist/m5tab/m5tab.component';
import { M6tabComponent } from './tablist/m6tab/m6tab.component';
import { SharedFormService } from './services/shared-form.service';

@NgModule({
  declarations: [
    CmdComponent,
    TablistComponent,
    M1tabComponent,
    M2tabComponent,
    M3tabComponent,
    M4tabComponent,
    M5tabComponent,
    M6tabComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    SharedModule,
    HomeRoutingModule
  ],
  exports:[
    HttpClientModule
  ],
  providers:[SharedFormService]
})
export class HomeModule { }
