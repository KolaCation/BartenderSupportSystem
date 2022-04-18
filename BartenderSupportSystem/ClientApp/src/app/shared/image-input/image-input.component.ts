import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'image-input',
  templateUrl: './image-input.component.html',
  styleUrls: ['./image-input.component.css'],
})
export class ImageInputComponent {
  selectedFile: File;
  @Input() pictureUrl: string = null;
  @Output() onFileSelected: EventEmitter<string> = new EventEmitter<string>();

  processFile(imageInput: any): void {
    this.selectedFile = <File>imageInput.files[0];
    const reader = new FileReader();
    if (this.selectedFile) {
      this.pictureUrl = null;
      reader.readAsDataURL(this.selectedFile);
      reader.onload = () => {
        const result: string = reader.result.toString();
        this.pictureUrl = result;
        this.onFileSelected.emit(this.pictureUrl);
      };
    }
  }
}
