import { Component, OnInit, ViewChild } from '@angular/core';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { Subscription, interval } from 'rxjs';
import { SharedFormService } from '../shared-form.service';
@Component({
  selector: 'app-tablist', 
  templateUrl: './tablist.component.html',
  styleUrl: './tablist.component.css'
})
export class TablistComponent implements OnInit {
  @ViewChild('staticTabs') tabset: TabsetComponent;
  subscription: Subscription;

  constructor(private sharedFormService: SharedFormService){
    
  }  
  ngOnInit(): void {
   // this.tabset.tabs[2].active=true;    
    this.subscription = interval(5000)
     .subscribe(() => {
      console.log(this.sharedFormService.currentData);
      });
  }
  activeTabId:string='cmd';
  changeTab($event) {
    this.activeTabId = $event?.id;
    console.log(this.activeTabId);
 }
}
/**change record to unrecord button */
// $('#recButton').addClass("notRec");

// $('#recButton').click(function(){
// 	if($('#recButton').hasClass('notRec')){
// 		$('#recButton').removeClass("notRec");
// 		$('#recButton').addClass("Rec");
// 	}
// 	else{
// 		$('#recButton').removeClass("Rec");
// 		$('#recButton').addClass("notRec");
// 	}
// });	