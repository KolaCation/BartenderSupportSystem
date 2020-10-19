import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MealListComponent } from '../meal-list/meal-list.component';
import { MealFormComponent } from '../meal-form/meal-form.component';

const routes: Routes = [
  { path: '', component: MealListComponent },
  { path: 'create', component: MealFormComponent },
  { path: 'edit/:id', component: MealFormComponent }
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
export class MealRoutingModule { }
