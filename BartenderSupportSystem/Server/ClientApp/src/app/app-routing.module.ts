import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
    canActivate: [AuthorizeGuard],
  },
  {
    path: 'brands',
    loadChildren: () =>
      import('./recommendationSystem/brands/brand/brand.module').then(
        (m) => m.BrandModule
      ),
    canActivate: [AuthorizeGuard],
  },
  {
    path: 'drinks',
    loadChildren: () =>
      import('./recommendationSystem/drinks/drink/drink.module').then(
        (m) => m.DrinkModule
      ),
    canActivate: [AuthorizeGuard],
  },
  {
    path: 'cocktails',
    loadChildren: () =>
      import('./recommendationSystem/cocktails/cocktail/cocktail.module').then(
        (m) => m.CocktailModule
      ),
    canActivate: [AuthorizeGuard],
  },
  {
    path: 'meals',
    loadChildren: () =>
      import('./recommendationSystem/meals/meal/meal.module').then(
        (m) => m.MealModule
      ),
    canActivate: [AuthorizeGuard],
  },
  {
    path: 'tests',
    loadChildren: () =>
      import('./testSystem/custom-test/custom-test.module').then(
        (m) => m.CustomTestModule
      ),
    canActivate: [AuthorizeGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
