import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CocktailDetailsComponent } from '../cocktail-details/cocktail-details.component';
import { CocktailFormComponent } from '../cocktail-form/cocktail-form.component';
import { CocktailListComponent } from '../cocktail-list/cocktail-list.component';

const routes: Routes = [
  { path: '', component: CocktailListComponent },
  { path: 'create', component: CocktailFormComponent },
  { path: ':id', component: CocktailDetailsComponent },
  { path: 'edit/:id', component: CocktailFormComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CocktailRoutingModule {}
