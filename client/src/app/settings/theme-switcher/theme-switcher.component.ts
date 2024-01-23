import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../core/services/theme.service';
import { SettingsService } from '../settings.service';
import { AccountService } from 'src/app/account/account.service';
import { UpdateUserSettingsDto } from 'src/app/shared/models/account/UpdateUserSettingsDto';
import { UserStoreService } from 'src/app/account/user-store.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-theme-switcher',
  templateUrl: './theme-switcher.component.html',
  styleUrls: ['./theme-switcher.component.scss']
})
export class ThemeSwitcherComponent implements OnInit {
  selectedTheme: string = '';
  isEdited: boolean = false;
  constructor(private settingsService: SettingsService,
    private themeService: ThemeService,
    private accountService: AccountService,
    private userStoreService: UserStoreService) {
    settingsService.selectSettingsPage('theme');
  }
  ngOnInit(): void {
    this.selectedTheme = this.currentTheme();
  }

  public currentTheme(): string {
    return this.themeService.current;
  }

  public selectTheme(value: string): void {
    const userSettings = this.accountService.getUserSettings();
    if (value !== userSettings?.theme)
      this.isEdited = true;
    else this.isEdited = false;
    this.themeService.current = value;
  }

  public cancel() {
    const userSettings = this.accountService.getUserSettings();
    this.isEdited = false;
    this.selectedTheme = userSettings?.theme!;
    this.themeService.current = userSettings?.theme!;
  }

  public submit() {
    const userSettings = this.accountService.getUserSettings();
    userSettings?.theme && (
      userSettings.theme = this.selectedTheme);
    const userSettingsDto: UpdateUserSettingsDto = {
      UserId: this.userStoreService.uid(),
      settings: userSettings!
    };
    this.accountService.updateSettings(userSettingsDto).subscribe({
      next: () => {
        const userSettingsJson = JSON.stringify(userSettings);
        localStorage.setItem('settings', userSettingsJson);
        this.isEdited = false;
      },
      error: (err: HttpErrorResponse) => {
        console.log(err);
      }
    });
  }
}