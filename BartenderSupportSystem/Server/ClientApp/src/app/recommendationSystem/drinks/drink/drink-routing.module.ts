import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DrinkListComponent } from '../drink-list/drink-list.component';
import { DrinkFormComponent } from '../drink-form/drink-form.component';
import { DrinkDetailsComponent } from '../drink-details/drink-details.component';

const routes: Routes = [
  { path: '', component: DrinkListComponent },
  { path: 'create', component: DrinkFormComponent },
  { path: ':id', component: DrinkDetailsComponent },
  { path: 'edit/:id', component: DrinkFormComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DrinkRoutingModule {}
