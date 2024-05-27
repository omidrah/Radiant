import { Component, OnInit, ViewChild } from '@angular/core';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { dataServicedotnet } from '../services/data.service-dotnet';
//import { dataService } from '../services/data.service';
@Component({
  selector: 'app-tablist',
  templateUrl: './tablist.component.html',
  styleUrl: './tablist.component.css'
})
export class TablistComponent implements OnInit {
  @ViewChild('staticTabs') tabset: TabsetComponent;

  //constructor(private dataservice:dataService){ }
  constructor(private dataservice:dataServicedotnet){ }

  ngOnInit(): void {
   // this.tabset.tabs[2].active=true;
   this.dataservice.sendDataToserver()
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
