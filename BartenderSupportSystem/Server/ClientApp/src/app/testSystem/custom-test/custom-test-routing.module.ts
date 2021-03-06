import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomTestFormComponent } from '../custom-test-form/custom-test-form.component';
import { CustomTestListComponent } from '../custom-test-list/custom-test-list.component';
import { CustomTestPassFormComponent } from '../custom-test-pass-form/custom-test-pass-form.component';
import { CustomTestResultListComponent } from '../custom-test-result-list/custom-test-result-list.component';
import { CustomTestResultComponent } from '../custom-test-result/custom-test-result.component';

const routes: Routes = [
  { path: '', component: CustomTestListComponent },
  { path: 'create', component: CustomTestFormComponent },
  { path: 'results', component: CustomTestResultListComponent },
  { path: ':id', component: CustomTestResultComponent },
  { path: 'edit/:id', component: CustomTestFormComponent },
  { path: 'pass/:id', component: CustomTestPassFormComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomTestRoutingModule {}
