import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavmenuComponent } from './navmenu/navmenu.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';
import { BrandService } from './recommendationSystem/brands/brand/brand.service';
import { DrinkService } from './recommendationSystem/drinks/drink/drink.service';
import { MealService } from './recommendationSystem/meals/meal/meal.service';
import { CocktailService } from './recommendationSystem/cocktails/cocktail/cocktail.service';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { ErrorHandlerService } from './shared/ErrorHandlerService';
import { CustomTestService } from './testSystem/custom-test/custom-test.service';
import { CustomTestResultService } from './testSystem/custom-test-result/custom-test-result.service';

@NgModule({
  declarations: [AppComponent, HomeComponent, NavmenuComponent],
  imports: [BrowserModule, AppRoutingModule, ApiAuthorizationModule],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    BrandService,
    DrinkService,
    MealService,
    CocktailService,
    AuthorizeService,
    ErrorHandlerService,
    CustomTestService,
    CustomTestResultService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
