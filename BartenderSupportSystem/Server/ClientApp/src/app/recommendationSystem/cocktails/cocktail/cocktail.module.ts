import { NgModule } from '@angular/core';
import { SharedModule } from '../../../shared/shared.module';
import { CocktailRoutingModule } from './cocktail-routing.module';
import { CocktailListComponent } from '../cocktail-list/cocktail-list.component';
import { CocktailDetailsComponent } from '../cocktail-details/cocktail-details.component';
import { CocktailFormComponent } from '../cocktail-form/cocktail-form.component';



@NgModule({
  declarations: [CocktailListComponent, CocktailDetailsComponent, CocktailFormComponent],
  imports: [
    SharedModule,
    CocktailRoutingModule
  ]
})
export class CocktailModule { }
