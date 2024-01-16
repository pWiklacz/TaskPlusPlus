import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {

  public static default = 'pulse';

  getBodyColor(): string {
    switch (this.current) {
      case 'slate':
        return '#aaa';
      case 'pulse':
        return '#444';
      default:
        return '#444';
    }
  }
  
  getInputBackgroundColor(): string {
    switch (this.current) {
      case 'slate':
        return '#272b30';
      case 'pulse':
        return '#ffffff';
      default:
        return '#ffffff';
    }
  }

  getColorPickerPanelBgColor(): string {
    switch (this.current) {
      case 'slate':
        return '#323232';
      case 'pulse':
        return '#ffffff';
      default:
        return '#ffffff';
    }
  }

  getPrimaryColor(): string {
    switch (this.current) {
      case 'slate':
        return '#3a3f44';
      case 'pulse':
        return '#593196';
      default:
        return '#593196';
    }
  }

  getSecondaryColor(): string {
    switch (this.current) {
      case 'slate':
        return '#7a8288';
      case 'pulse':
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
      this.style.href = `/${this.current}.css`;
    }
  }
}