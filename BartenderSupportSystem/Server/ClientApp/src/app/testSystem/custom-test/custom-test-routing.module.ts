import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomTestDetailsComponent } from '../custom-test-details/custom-test-details.component';
import { CustomTestFormComponent } from '../custom-test-form/custom-test-form.component';
import { CustomTestListComponent } from '../custom-test-list/custom-test-list.component';

const routes: Routes = [
  { path: '', component: CustomTestListComponent },
  { path: 'create', component: CustomTestFormComponent },
  { path: ':id', component: CustomTestDetailsComponent },
  { path: 'edit/:id', component: CustomTestFormComponent }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CustomTestRoutingModule { }
