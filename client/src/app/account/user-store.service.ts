import { Injectable, signal } from '@angular/core';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {
  firstName = signal<string>('');
  lastName = signal<string>('');
  email = signal<string>('');
  private userPayload: any;

  constructor() {
 
   }

  setFirstName(value: string) {
    this.firstName.update(() => value);
  }

  setLastName(value: string) {
    this.lastName.update(() => value);
  }

  setEmail(value: string) {
    this.email.update(() => value);
  }

  clearSignals() {
    this.firstName.update(() => '');
    this.lastName.update(() => '');
    this.email.update(() => '');
  }
  
}
 