import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: null },
  { path: 'create', component: null },
  { path: ':id', component: null },
  { path: 'edit/:id', component: null }
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
export class CocktailRoutingModule { }
