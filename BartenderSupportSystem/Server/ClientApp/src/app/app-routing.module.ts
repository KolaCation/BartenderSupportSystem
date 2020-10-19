import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DataComponent } from './data/data.component';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'data', component: DataComponent, canActivate: [AuthorizeGuard] },
  { path: 'brands', loadChildren: () => import('./recommendationSystem/brands/brand/brand.module').then(m => m.BrandModule) },
  { path: 'drinks', loadChildren: () => import('./recommendationSystem/drinks/drink/drink.module').then(m => m.DrinkModule) },
  { path: 'cocktails', loadChildren: () => import('./recommendationSystem/cocktails/cocktail/cocktail.module').then(m => m.CocktailModule) },
  { path: 'meals', loadChildren: () => import('./recommendationSystem/meals/meal/meal.module').then(m => m.MealModule) },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
