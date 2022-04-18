import { NgModule } from '@angular/core';
import { MealRoutingModule } from './meal-routing.module';
import { MealListComponent } from '../meal-list/meal-list.component';
import { MealFormComponent } from '../meal-form/meal-form.component';
import { SharedModule } from '../../../shared/shared.module';

@NgModule({
  declarations: [MealListComponent, MealFormComponent],
  imports: [SharedModule, MealRoutingModule],
})
export class MealModule {}
