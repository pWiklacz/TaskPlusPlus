import { Component, Input, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/category/category.service';
import { CategoryDto } from 'src/app/shared/models/CategoryDto';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {
//@Input() sideNavStatus: boolean = true;
userCategories: CategoryDto[] = [];

systemCategories = [
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
  },
  {
    number: '3',
    name: 'Calendar',
    icon: 'fa-solid fa-calendar',
    color: '#cb063e'
  },
  {
    number: '4',
    name: 'Next Actions',
    icon: 'fa-solid fa-angles-right',
    color: '#f37b16'
  },
  {
    number: '5',
    name: 'Projects',
    icon: 'fa-solid fa-list-check',
    color: '#ffffff'
  },
  {
    number: '6',
    name: 'Waiting For',
    icon: 'fa-solid fa-hourglass-half',
    color: '#000000'
  },
  {
    number: '7',
    name: 'Someday/Maybe',
    icon: 'fa-solid fa-lightbulb',
    color: '#f3f316'
  }
]; 

  constructor(public categoryService: CategoryService) {}

  ngOnInit(): void {
    this.getCategories()
  }

  selectCategory(id: number){
    this.categoryService.selecetCategory(id);
  }

  getCategories() {
    this.categoryService.getCategories().subscribe({
      error: error => console.log(error)     
    })
  }

}
