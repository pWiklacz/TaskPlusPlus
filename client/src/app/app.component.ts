import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { MessageService } from 'primeng/api';
import { ThemeService } from './core/services/theme.service';
import { BusyService } from './core/services/busy.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit{
  contentLoaded: boolean = false;
  isLoggedIn: boolean = false;
  sideNavStatus: boolean = false;

  constructor(private themeService: ThemeService, private busyService: BusyService,public accountService: AccountService) { }
  ngOnInit(): void {

    this.busyService.busy()
    this.loadData();
  }

  loadData() {
    setTimeout(() => {
      this.accountService.isLoggedIn$.subscribe(res =>{
        this.isLoggedIn = this.accountService.isLoggedIn();
      })
      this.contentLoaded = true;
      this.busyService.idle();
    }, 1000);
  }
}
