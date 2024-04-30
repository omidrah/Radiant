import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { RootRoutingModule } from './root-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RootComponent } from './root.component';


@NgModule({
  imports: [
    CommonModule,
    BrowserModule,   
    BrowserAnimationsModule,  
    RootRoutingModule,
  ],
  declarations: [RootComponent],
  bootstrap: [RootComponent],
})
export class RootModule { }
