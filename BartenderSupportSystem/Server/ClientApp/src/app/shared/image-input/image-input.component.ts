import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'image-input',
  templateUrl: './image-input.component.html',
  styleUrls: ['./image-input.component.css']
})
export class ImageInputComponent implements OnInit {

  selectedFile: File;
  pictureUrl: string = null;

  constructor() { }

  ngOnInit(): void {
  }

  processFile(imageInput: any) {
    this.selectedFile = <File>imageInput.files[0];
    const reader = new FileReader();
    console.log(this.selectedFile);
    if (this.selectedFile) {
      reader.readAsDataURL(this.selectedFile);
      reader.onload = () => this.pictureUrl = reader.result.toString();
    }
  }
}
