import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  // providers: [MessageService]
})
export class AppComponent implements OnInit{
  isLoggedIn: boolean = false;

  constructor(public accountService: AccountService) { }
  ngOnInit(): void {
    this.accountService.isLoggedIn$.subscribe(res =>{
      this.isLoggedIn = this.accountService.isLoggedIn();
    })
  }
}
