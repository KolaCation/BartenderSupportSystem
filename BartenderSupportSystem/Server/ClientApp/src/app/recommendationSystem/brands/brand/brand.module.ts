import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrandFormComponent } from '../brand-form/brand-form.component';
import { BrandListComponent } from '../brand-list/brand-list.component';
import { BrandRoutingModule } from './brand-routing.module';
import { SharedModule } from '../../../shared/shared.module';



@NgModule({
  declarations: [BrandFormComponent, BrandListComponent],
  imports: [
    CommonModule,
    BrandRoutingModule,
    SharedModule
  ]
})
export class BrandModule { }
