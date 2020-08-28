import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrandFormComponent } from '../brand-form/brand-form.component';

const routes: Routes = [
  { path: 'create', component: BrandFormComponent }
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
export class BrandRoutingModule { }
