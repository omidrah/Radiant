import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { CLASS_LIST, DAYS_SHORT } from '../util/calendar.constants';
import { Subscription, timer } from 'rxjs';
import { UtilService } from '../util/UtilService';

@Component({
  selector: 'app-digiclock',  
  templateUrl: './digiclock.component.html',
  styleUrl: './digiclock.component.css'
})
export class DigiclockComponent  implements OnInit, OnDestroy {
  public today: string;

  public borders: string[];
  public monthYear: string;  
  public meridian: string;  
  @Input()
  public displayDots: boolean = false;  
  public days: string[] = DAYS_SHORT;  
  public timeFormatList: string[] = [];  
  public subscriptions: Subscription[] = []; 

  constructor(private util: UtilService) {
    this.borders = 'd1 d2 d3 d4 d5 d6 d7'.split(' ');
    this._init();
  }

  public ngOnInit(): void {
    this.subscriptions.push(
      timer(0, 1000)
        .subscribe((t) => {
          this._init();
        })
    );
  }
   public ngOnDestroy(): void {
    for (let subscription of this.subscriptions) {
      if (!!subscription) {
        subscription.unsubscribe();
      }
    }
  }
  private _run(_meridian: string): void {
    const d: string[] = (new Date())
      .toDateString()
      .split(' ');

    // toggles the dots
    this.displayDots = !this.displayDots;
    // sets the day today
    this.today = (d[0] || '').toUpperCase();
    // sets the month & year
    this.monthYear = `${d[1]} ${d[2]}, ${d[3]}`;
    // sets am/pm
    this.meridian = (_meridian || '').toUpperCase();
  }

  private _init(): void {
    const now: Date = new Date();
    const t: string[] = now.toLocaleTimeString().split(' ');
    if (Array.isArray(t) && t[0]) {
      let digits: any = t[0]
        .split(':')
        .map(v => this.util.to2Digit(v))
        .join(':')
        .split('');
      this.timeFormatList = [];
      for (const i of digits) {
        this.timeFormatList.push(CLASS_LIST[i]);
      }
      // run the other options.
      this._run(t[1]);
    }
  }
}
