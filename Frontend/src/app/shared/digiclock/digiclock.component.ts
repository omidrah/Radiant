import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription, timer } from 'rxjs';

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
  public inputTime: string; // Added inputTime to bind with input field
  private subscriptions: Subscription[] = [];
  private customTime: Date | null = null; // Added customTime to hold user-set time

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
    const now = this.customTime || new Date();
    this.currentTime = now.toLocaleTimeString('en-US', { hour12: true });
    this.currentDate = now.toDateString();
    this.today = this.days[now.getDay()];
  }

  setTime(): void {
    if (this.inputTime) {
      const [hours, minutes] = this.inputTime.split(':').map(Number);
      this.customTime = new Date();
      this.customTime.setHours(hours, minutes, 0);
      this.updateTime(); // Update the display immediately after setting new time
    }
  }
}
