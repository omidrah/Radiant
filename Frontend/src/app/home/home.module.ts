import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { CmdComponent } from './tablist/cmd/cmd.component';
import { TablistComponent } from './tablist/tablist.component';
import { M1tabComponent } from './tablist/m1tab/m1tab.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    CmdComponent,
    TablistComponent,
    M1tabComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpClientJsonpModule,  
    SharedModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
