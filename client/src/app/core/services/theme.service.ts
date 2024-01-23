import { Injectable } from '@angular/core';
import { UserSettings } from 'src/app/shared/models/account/user';

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
      case 'united':
        return '#212529';
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
      case 'united':
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
      case 'united':
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
      case 'united':
        return '#e95420';
      case 'lumen':
        return '#158cba';
      default:
        return '#593196';
    }
  }

  getBorderColor(): string {
    switch (this.current) {
      case 'slate':
        return '#282d32';
      case 'pulse':
        return '#ededed';
      case 'united':
        return '#ededed';
      case 'lumen':
        return '#ededed';
      default:
        return '#ededed';
    }
  }

  getSecondaryColor(): string {
    switch (this.current) {
      case 'slate':
        return '#7a8288';
      case 'pulse':
        return '#a991d4';
      case 'united':
        return '#aea79f';
      case 'lumen':
        return '#75caeb';
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