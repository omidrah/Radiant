import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { CLASS_LIST, DAYS_SHORT } from '../util/calendar.constants';
import { Subscription, timer } from 'rxjs';
import { UtilService } from '../util/UtilService';

@Component({
  selector: 'app-digiclock',
  templateUrl: './digiclock.component.html',
  styleUrl: './digiclock.component.css'
})
export class DigiclockComponent implements OnInit, OnDestroy {
  public currentTime: string;
  public currentDate: string;
  public today: string;
  public days: string[] = ['SUN', 'MON', 'TUE', 'WED', 'THU', 'FRI', 'SAT'];
  private subscriptions: Subscription[] = [];

  constructor() {}

  ngOnInit(): void {
    this.updateTime();
    this.subscriptions.push(
      timer(0, 1000).subscribe(() => {
        this.updateTime();
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(subscription => subscription.unsubscribe());
  }

  private updateTime(): void {
    const now = new Date();
    this.currentTime = now.toLocaleTimeString('en-US', { hour12: true });
    this.currentDate = now.toDateString();
    this.today = this.days[now.getDay()];
  }

  setTime(): void {
    console.log('Set button clicked');
  }
}
