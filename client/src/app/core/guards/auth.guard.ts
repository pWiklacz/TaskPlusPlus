import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from 'src/app/account/account.service';

export const authGuard: CanActivateFn = async (route, state) => {
  const token = localStorage.getItem('token');
  const accountService = inject(AccountService);

  if (token && !accountService.isTokenExpired()) {

    return true;
  }
  const isRefreshSuccess = await accountService.tryRefreshingTokens(token!);
  if (!isRefreshSuccess) { 
    accountService.logout(); 
  }
  return isRefreshSuccess;
};
