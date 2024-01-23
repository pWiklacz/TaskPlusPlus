import { Injectable, signal } from '@angular/core';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {
  firstName = signal<string>('');
  lastName = signal<string>('');
  email = signal<string>('');
  uid = signal<string>('');
  hasPassword = signal<boolean>(false);
  constructor() {

  }

  setFirstName(value: string) {
    this.firstName.update(() => value);
  }

  setHasPassword(value: boolean) {
    this.hasPassword.update(() => value);
  }

  setLastName(value: string) {
    this.lastName.update(() => value);
  }

  setEmail(value: string) {
    this.email.update(() => value);
  }

  setUId(value: string) {
    this.uid.update(() => value);
  }

  clearSignals() {
    this.firstName.update(() => '');
    this.lastName.update(() => '');
    this.email.update(() => '');
    this.uid.update(() => '');
    this.hasPassword.update(() => false);
  }
}
