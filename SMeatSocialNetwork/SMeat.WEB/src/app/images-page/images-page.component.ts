import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'images-page',
  templateUrl: './images-page.component.html',
  styleUrls: ['./images-page.component.css']
})
export class ImagesPageComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  onImageLoaded() { // update user info after modal method - uploadFile in image-modal.component finishes with success
    //this.getUserInfo(this.id);
  }

}
