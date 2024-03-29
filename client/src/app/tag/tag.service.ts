import { Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TagDto } from '../shared/models/tag/TagDto';
import { HttpClient } from '@angular/common/http';
import { ApiResponse } from '../shared/models/ApiResponse';
import { map } from 'rxjs';
import { CreateTagDto } from '../shared/models/tag/CreateTagDto';

@Injectable({
  providedIn: 'root'
})
export class TagService {
  apiUrl = environment.apiUrl;
  public userTags = signal<TagDto[]>([]);

  constructor(private http: HttpClient) { }

  addTag(tag: TagDto) {
    this.userTags.mutate((val) => {
      val.push(tag);
    })
  }

  removeTag(id: number) {
    this.userTags.mutate((val) => {
      const index = val.findIndex(t => t.id == id)
      if (index !== -1) {
        val.splice(index, 1);
      }
    })
  }


  updateTag(tag: TagDto) {
    this.userTags.mutate((val) => {
      const index = val.findIndex(t => t.id == tag.id)
      if (index !== -1) {
        val[index] = tag;
      }
    })
  }

  filterTagsByTagIds(selectedTagIds: number[]): TagDto[] {
    return this.userTags().filter(tag => selectedTagIds.includes(tag.id));
  }

  getTags() {
    if (this.userTags().length == 0) {
      return this.http.get<ApiResponse<TagDto[]>>(this.apiUrl + 'Tag').pipe(
        map(tags => {
          this.userTags.set(tags.value)
        })
      )
    }
    return
  }

  postTag(values: CreateTagDto) {
    return this.http.post<ApiResponse<number>>(this.apiUrl + 'Tag', values);
  }

  getTag(id: number) {
    return this.http.get<ApiResponse<TagDto>>(this.apiUrl + 'Tag/' + id);
  }

  deleteTag(id: number) {
    return this.http.delete(this.apiUrl + 'Tag/' + id);
  }

  putTag(updatedTag: TagDto) {
    return this.http.put(this.apiUrl + 'Tag/' + updatedTag.id, updatedTag);
  }
}
