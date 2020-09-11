import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ImageInputComponent } from './image-input/image-input.component';

@NgModule({
  imports: [CommonModule],
  declarations: [ImageInputComponent],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    ImageInputComponent
  ]
})
export class SharedModule { }
