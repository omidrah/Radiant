import { Injectable } from '@angular/core';
import { Observable, interval, switchMap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SharedFormService } from './shared-form.service';

@Injectable({
  providedIn: 'root'
})
export class dataService {

  private apiUrl = 'http://localhost:3000'; // Replace with your actual backend API URL

  constructor(private http: HttpClient,private sharedFormService:SharedFormService) {}

  sendDataToserver() {

      // this.subscription =
       interval(5000)
       .subscribe(() => {
         this.sharedFormService.currentData.subscribe(c=>{
          this.http.post(`${this.apiUrl}/api/save-data`, c).subscribe
                (
                  (response) => {
                    console.log('Data saved successfully!', response);
                  },
                  (error) => {
                    console.error('Error saving data:', error);
                  }
                );
          })
         });

    // return interval(5000).pipe(
    //   // Emit data every 5 seconds
    //   // You can replace this with your actual data source logic
    //   // For example, fetch data from a component or another service
    //   // and send it to the backend
    //   // Example: this.http.get<any>(this.apiUrl)
    //   // Adjust the data as needed

    //   switchMap(() => this.sharedFormService.currentData.pipe(
    //     switchMap(c => this.http.post(`http://${this.apiUrl}/api/save-data`, c))
    //   ))
    // )
  }
}
