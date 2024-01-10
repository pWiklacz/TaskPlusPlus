import { Component, HostListener, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';
import { MessageService } from 'primeng/api';
import { ThemeService } from './core/services/theme.service';
import { BusyService } from './core/services/busy.service';
import { ViewportScroller } from '@angular/common';
import { SideNavService } from './core/services/side-nav.service';
import { TagService } from './tag/tag.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  contentLoaded: boolean = false;
  isLoggedIn: boolean = false;
  isSmallScreen = false;

  constructor(private themeService: ThemeService, private busyService: BusyService,
    public accountService: AccountService,
    public sideNavService: SideNavService) { }

  ngOnInit(): void {
    this.accountService.isLoggedIn$.subscribe(res => {
      this.isLoggedIn = this.accountService.isLoggedIn();
    })

    this.contentLoaded = true;

    this.checkScreenSize();

    window.addEventListener('resize', () => {
      this.checkScreenSize();
    });
  }

  private checkScreenSize() {

    const screenWidth = window.innerWidth;

    if (screenWidth < 750) {
      if (this.sideNavService.sidNavOpenStatus()) {
        this.sideNavService.updateSideNavStatus();
      }
    }
  }

  closeSideNav() {
    this.sideNavService.updateSideNavStatus();
  }

  // ngOnInit(): void {

  //   this.busyService.busy()
  //   this.loadData();
  //   this.checkScreenSize();

  //   window.addEventListener('resize', () => {
  //     this.checkScreenSize();
  //   });
  // }

  loadData() {
    setTimeout(() => {
      this.accountService.isLoggedIn$.subscribe(res => {
        this.isLoggedIn = this.accountService.isLoggedIn();
      })
      this.contentLoaded = true;
      this.busyService.idle();
    }, 1000);
  }
}
