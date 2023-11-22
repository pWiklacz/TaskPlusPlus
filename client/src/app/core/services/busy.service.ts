import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ThemeService } from './theme.service';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerService, private themeService: ThemeService) { }

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'square-jelly-box',
      bdColor: this.themeService.getPrimaryColor(),
      color: this.themeService.getSecondaryColor()
    })
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
