import { Component,  OnInit, ViewChild } from '@angular/core';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { SharedFormService } from '../services/shared-form.service';
import { Constants } from '../../config/constants';
@Component({
  selector: 'app-tablist',
  templateUrl: './tablist.component.html',
  styleUrl: './tablist.component.css'
})
export class TablistComponent implements OnInit  {
  @ViewChild('staticTabs') tabset: TabsetComponent;
  title = Constants.TitleOfSite; 
  apiUrl = Constants.API_ENDPOINT;
  activeTabId:string='cmd';

 constructor(private sharedService:SharedFormService){ 
  console.log(Constants.API_ENDPOINT); 
 }

  ngOnInit(): void {
   // this.tabset.tabs[2].active=true;
  this.sharedService.SendDataByTimer(this.apiUrl,1000);
  }
  changeTab($event) {
    this.activeTabId = $event?.id;
   // console.log(this.activeTabId);
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
