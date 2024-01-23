import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public date: Date = new Date();
  public year: number = this.date.getFullYear();
  isLoggedIn: boolean = false;

  constructor(public accountService: AccountService,
    private router: Router){}

  ngOnInit(): void {
  this.accountService.isLoggedIn$.subscribe(res => {
      this.isLoggedIn = this.accountService.isLoggedIn();
    })
    if(this.isLoggedIn)
    {
    const userSettings = this.accountService.getUserSettings();
    this.router.navigate([`app/dashboard/${userSettings!.startPage.toLowerCase()}`]);
    }
  }


}
