import { Component, OnInit } from '@angular/core';
import { TagService } from '../tag.service';

@Component({
  selector: 'app-tag-list',
  templateUrl: './tag-list.component.html',
  styleUrls: ['./tag-list.component.scss']
})
export class TagListComponent implements OnInit {

  constructor(public tagService: TagService) { }

  ngOnInit(): void {
    this.tagService.getTags()?.subscribe({
      error: error => console.log(error)
    })

  }
}
