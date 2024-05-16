// shared-form.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedFormService {
  private formData = new BehaviorSubject<any>({});
  currentData = this.formData.asObservable();

  constructor() {}

  updateFormData(newData: any) {
    this.formData.next(newData);
  }

  saveFormData() {
    // Logic to save formData to a file
     const blob = new Blob([JSON.stringify(this.currentData)], { type: 'application/json' });
          // Use FileSaver or similar library to trigger the download
    // FileSaver.saveAs(blob, 'data.json');
  }
}
