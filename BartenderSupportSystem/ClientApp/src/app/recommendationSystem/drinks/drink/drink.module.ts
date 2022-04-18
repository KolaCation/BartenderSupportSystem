import { NgModule } from '@angular/core';
import { DrinkRoutingModule } from './drink-routing.module';
import { DrinkListComponent } from '../drink-list/drink-list.component';
import { DrinkFormComponent } from '../drink-form/drink-form.component';
import { SharedModule } from '../../../shared/shared.module';
import { DrinkDetailsComponent } from '../drink-details/drink-details.component';
import { DrinkFilterComponent } from '../drink-filter/drink-filter.component';

@NgModule({
  declarations: [
    DrinkListComponent,
    DrinkFormComponent,
    DrinkDetailsComponent,
    DrinkFilterComponent,
  ],
  imports: [SharedModule, DrinkRoutingModule],
})
export class DrinkModule {}
