import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule, FaIconLibrary } from '@fortawesome/angular-fontawesome';
import { faHouse, faPlus, faKey, faBars } from '@fortawesome/free-solid-svg-icons';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { TokenInterceptor } from './core/interceptors/token.interceptor';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { SocialLoginModule, SocialAuthServiceConfig, FacebookLoginProvider } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';
import { environment } from 'src/environments/environment';
import { ThemeService } from './core/services/theme.service';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SettingsModule } from './settings/settings.module';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { CategoryModule } from './category/category.module';
import { FormsModule } from '@angular/forms';
import { TaskModule } from './task/task.module';
import { TagModule } from './tag/tag.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ProjectModule } from './project/project.module';



@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    HttpClientModule,
    HomeModule,
    CoreModule,
    ToastModule,
    SocialLoginModule,
    SettingsModule,
    ModalModule.forRoot(),
    CategoryModule,
    FormsModule,
    TaskModule,
    TagModule,
    DashboardModule,
    ButtonsModule.forRoot(),
    ProjectModule,
    SettingsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true },
    [MessageService],
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(environment.googleClientId)
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('884218602947918')
          }
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    },
    ThemeService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(library: FaIconLibrary) {
    library.addIcons(faHouse, faPlus, faKey, faBars)
  }
}
