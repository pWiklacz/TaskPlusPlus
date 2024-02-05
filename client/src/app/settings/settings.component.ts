import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterStateSnapshot } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SettingsService } from './settings.service';
import { ThemeService } from '../core/services/theme.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

  constructor(public bsModalRef: BsModalRef,
    private router: Router,
    public settingsService: SettingsService,
    private themeService: ThemeService) { }

  ngOnInit() {
    document.documentElement.style.setProperty('--border-color',
    this.themeService.getBorderColor());
  }

  closeModal() {
    this.router.navigate(
      [
        "/app",
        {
          outlets: {
            settings: null
          }
        }
      ]
    ).then(() => {
      this.settingsService.setOpenState(false)
      this.settingsService.selectSettingsPage(null);
      this.bsModalRef.hide();
    });
  }
}
