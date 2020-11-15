import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { CustomTestRoutingModule } from './custom-test-routing.module';
import { CustomTestListComponent } from '../custom-test-list/custom-test-list.component';
import { CustomTestFormComponent } from '../custom-test-form/custom-test-form.component';
import { CustomTestDetailsComponent } from '../custom-test-details/custom-test-details.component';



@NgModule({
  declarations: [CustomTestListComponent, CustomTestFormComponent, CustomTestDetailsComponent],
  imports: [
    SharedModule,
    CustomTestRoutingModule
  ]
})
export class CustomTestModule { }
