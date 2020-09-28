import { ThrowStmt } from '@angular/compiler';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'image-input',
  templateUrl: './image-input.component.html',
  styleUrls: ['./image-input.component.css']
})
export class ImageInputComponent implements OnInit {

  selectedFile: File;
  @Input() pictureUrl: string = null;
  @Output() onFileSelected: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  processFile(imageInput: any) {
    this.selectedFile = <File>imageInput.files[0];
    const reader = new FileReader();
    if (this.selectedFile) {
      this.pictureUrl = null;
      reader.readAsDataURL(this.selectedFile);
      reader.onload = () => {
        let result: string = reader.result.toString();
        this.pictureUrl = result;
        this.onFileSelected.emit(this.pictureUrl);
      }
    }
  }
}
