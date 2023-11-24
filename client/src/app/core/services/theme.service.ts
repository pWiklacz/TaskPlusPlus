import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  public static default = 'default';

  getColorPickerPanelBgColor(): string {
    switch (this.current) {
      case 'slate':
        return '#323232';
      case 'default':
        return '#ffffff';
      default:
        return '#ffffff';
    }
  }

  getPrimaryColor(): string {
    switch (this.current) {
      case 'slate':
        return '#3a3f44';
      case 'default':
        return '#593196';
      default:
        return '#593196';
    }
  }

  getSecondaryColor(): string {
    switch (this.current) {
      case 'slate':
        return '#7a8288';
      case 'default':
        return '#a991d4';
      default:
        return '#a991d4';
    }
  }

  public get current(): string {
    return localStorage.getItem('theme') ?? ThemeService.default;
  }

  public set current(value: string) {
    localStorage.setItem('theme', value);
    this.style.href = `/${value}.css`;
  }

  private readonly style: HTMLLinkElement;

  constructor() {
    this.style = document.createElement('link');
    this.style.rel = 'stylesheet';
    document.head.appendChild(this.style);

    if (localStorage.getItem('theme') !== undefined) {
      console.log(this.current);
      this.style.href = `/${this.current}.css`;
    }
  }
}