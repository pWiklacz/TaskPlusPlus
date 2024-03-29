import { Injectable, signal } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {
  selectedSettingsPage = signal<string | null>(null);
  showUpdateButton = signal<boolean>(false);
  private isOpenSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  isOpen$: Observable<boolean> = this.isOpenSubject.asObservable();
  constructor() { }

  selectSettingsPage(pageName: string | null) {
    this.selectedSettingsPage.update(() => pageName);
  }

  changeShowUpdateButtonState(value: boolean) {
    this.showUpdateButton.update(() => value);
  }

  setOpenState(isOpen: boolean): void {
    this.isOpenSubject.next(isOpen);
  }

  get isOpen(): boolean {
    return this.isOpenSubject.value;
  }
}
