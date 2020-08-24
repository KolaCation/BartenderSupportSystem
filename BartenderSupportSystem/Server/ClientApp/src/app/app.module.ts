import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DataComponent } from './data/data.component';
import { HomeComponent } from './home/home.component';
import { NavmenuComponent } from './navmenu/navmenu.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    DataComponent,
    HomeComponent,
    NavmenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiAuthorizationModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
