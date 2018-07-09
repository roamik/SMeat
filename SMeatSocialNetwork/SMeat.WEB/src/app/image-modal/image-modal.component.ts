import { Component, OnInit, EventEmitter, Output, ViewChild, TemplateRef, Input } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { FormGroup } from '@angular/forms';
import { UploadService } from '../_services/upload.service';
import { UsersService } from '../_services/users.service';
import { BaseTosterService } from '../_services/base-toaster.service';
import { Router } from '@angular/router';

@Component({
  selector: 'image-modal',
  templateUrl: './image-modal.component.html',
  styleUrls: ['./image-modal.component.css']
})
export class ImageModalComponent implements OnInit {

  @ViewChild('template')
  template: TemplateRef<any>;

  @Output()
  change: EventEmitter<User> = new EventEmitter<User>();

  form: FormGroup;

  fileToUpload: any;

  imageUrl: string;

  fileInput: any;

  checkRoute: any;

  user: User = new User();

  @Input() // receive user id from profile page in order to have user info in the modal (if needed)
  set userId(value: string) {
    this.user = new User();
    this.user.id = value;
  }

  modalRef: BsModalRef;

  constructor(private modalService: BsModalService,
    private usersService: UsersService,
    private uploadService: UploadService,
    private tosterService: BaseTosterService,
    private router: Router) { }

  ngOnInit() {
    this.checkRoute = this.router;
  }

  //this.router.url === '/login'

  uploadUserImage() { //DUPLICATEDCODE - for setting the avatar pic directly for a user
    this.uploadService.uploadUserImage(this.fileToUpload)
      .subscribe(res => {
        //this.modalRef.hide();
        this.change.emit();
        this.imageUrl = res.pictureUrl;
        this.tosterService.success();
      },
      error => {
        this.tosterService.error();
      });
  }

  uploadImage() { // DUPLICATEDCODE - global loading 
    this.uploadService.uploadImage(this.fileToUpload)
      .subscribe(res => {
        //this.modalRef.hide();
        this.change.emit();
        this.imageUrl = res.path;
        this.tosterService.success();
      },
        error => {
          this.tosterService.error();
        });
  }

  onFileChange(event) {
    let file = event.target.files[0];
    this.fileToUpload = file;
  }

  // uncomment all of the below if you need to add user settings to image-modal
  //(f.e additional settings that are not on the user-settings page)

  //afterUserIdSet() {
  //  this.getUserInfo(this.user.id);
  //}

  
  //updateUserInfo() {
  //  this.usersService.update(this.user)
  //    .subscribe(
  //    response => {
  //      this.user = response;

  //      this.modalRef.hide();
  //    },
  //    error => {
  //    });

  //}

  getUserInfo() {
    this.usersService.getById(this.user.id).subscribe(
      response => { this.imageUrl = response.pictureUrl },
      error => { }
    );
  }

  open() {
    this.modalRef = this.modalService.show(this.template);
    //this.getUserInfo();
    //this.afterUserIdSet(); // uncoment this too, it's part of the methods above
  }

}
