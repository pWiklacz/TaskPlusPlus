import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../core/services/theme.service';

@Component({
  selector: 'app-theme-switcher',
  templateUrl: './theme-switcher.component.html',
  styleUrls: ['./theme-switcher.component.scss']
})
export class ThemeSwitcherComponent {

  constructor(private theme: ThemeService) { }

  public currentTheme(): string {
    return this.theme.current;
  }

  public selectTheme(value: string): void {
    this.theme.current = value;
  }
}