import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SideNavService {
  public sidNavOpenStatus = signal(true);

  constructor() { }

  updateSideNavStatus() {
    if (this.sidNavOpenStatus()) {
      this.sidNavOpenStatus.set(false)
    }
    else {
      this.sidNavOpenStatus.set(true)
    }
  }
}
