import { NgModule } from '@angular/core';
import { DrinkRoutingModule } from './drink-routing.module';
import { DrinkListComponent } from '../drink-list/drink-list.component';
import { DrinkFormComponent } from '../drink-form/drink-form.component';
import { SharedModule } from '../../../shared/shared.module';



@NgModule({
  declarations: [DrinkListComponent, DrinkFormComponent],
  imports: [
    SharedModule,
    DrinkRoutingModule
  ]
})
export class DrinkModule { }
