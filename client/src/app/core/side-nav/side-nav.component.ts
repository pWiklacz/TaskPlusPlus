import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {
@Input() sideNavStatus: boolean = false;
list = [
  {
    number: '1',
    name: 'Inbox',
    icon: 'fa-solid fa-inbox',
    color: '#4cd6f1' 
  },
  {
    number: '2',
    name: 'Today',
    icon: 'fa-solid fa-calendar-day',
    color: '#065535'
  }
];

  constructor() {}
  ngOnInit(): void {
  }

}
