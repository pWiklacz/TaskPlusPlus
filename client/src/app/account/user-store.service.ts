import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {
  firstName = signal<string>('');
  lastName = signal<string>('');
  email = signal<string>('');

  constructor() { }

  setFirstName(value: string) {
    this.firstName.update(() => value);
  }

  setLastName(value: string) {
    this.lastName.update(() => value);
  }

  setEmail(value: string) {
    this.email.update(() => value);
  }
  
}
 