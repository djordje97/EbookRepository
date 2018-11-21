import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { NgForm } from '@angular/forms';
import { Alert } from 'selenium-webdriver';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  tryGetUser;
  user={
    'username':'',
    'firstname':'',
    'lastname':'',
    'password':'',
    'categoryId':1
  };
  usernameError=false;
  success=false;
  otherError=false;
  @ViewChild('f')ngform:NgForm;
  constructor(private userService:UserService) { }

  ngOnInit() {
  }

  register(){
    console.log(this.user)
    this.userService.getUser(this.user.username).subscribe(response =>{
        
        this.usernameError=true
    },error =>{
      if(error.status == 404){
        this.userService.register(this.user).subscribe(data =>{
          this.success=true;
          this.ngform.reset();
        });
      }else{
        this.otherError=true;
      }
    });
  }

  focus(){
    this.usernameError=false;
  }
}
