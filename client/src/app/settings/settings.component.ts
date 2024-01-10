import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterStateSnapshot } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { SettingsService } from './settings.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

  constructor(public bsModalRef: BsModalRef,
    private router: Router,
    public settingsService: SettingsService) { }

  ngOnInit() {
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
