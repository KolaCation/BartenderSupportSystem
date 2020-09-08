import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BrandFormComponent } from '../brand-form/brand-form.component';
import { BrandListComponent } from '../brand-list/brand-list.component';

const routes: Routes = [
  { path: '', component: BrandListComponent },
  { path: 'create', component: BrandFormComponent },
  { path: 'edit/:id', component: BrandFormComponent }
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
