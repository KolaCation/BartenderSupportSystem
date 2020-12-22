import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { CustomTestRoutingModule } from './custom-test-routing.module';
import { CustomTestListComponent } from '../custom-test-list/custom-test-list.component';
import { CustomTestFormComponent } from '../custom-test-form/custom-test-form.component';
import { CustomTestPassFormComponent } from '../custom-test-pass-form/custom-test-pass-form.component';
import { CustomTestResultComponent } from '../custom-test-result/custom-test-result.component';
import { RatingComponent } from '../rating/rating.component';

@NgModule({
  declarations: [
    CustomTestListComponent,
    CustomTestFormComponent,
    CustomTestPassFormComponent,
    CustomTestResultComponent,
    RatingComponent,
  ],
  imports: [SharedModule, CustomTestRoutingModule],
})
export class CustomTestModule {}
