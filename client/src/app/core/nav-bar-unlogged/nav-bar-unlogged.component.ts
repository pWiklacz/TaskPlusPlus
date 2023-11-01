import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar-unlogged',
  templateUrl: './nav-bar-unlogged.component.html',
  styleUrls: ['./nav-bar-unlogged.component.scss']
})
export class NavBarUnloggedComponent {

  constructor(public accountService: AccountService){}
}
