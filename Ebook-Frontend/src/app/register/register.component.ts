import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { NgForm } from '@angular/forms';
import { Alert } from 'selenium-webdriver';
import { CategoryService } from '../services/category/category.service';

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
    'categoryId':''
  };
categories;
  usernameError=false;
  success=false;
  otherError=false;
  @ViewChild('f')ngform:NgForm;
  constructor(private userService:UserService,private categoryService:CategoryService) { }

  ngOnInit() {
    this.categoryService.getAllCategories().subscribe(data =>{
      this.categories=data;
     
      
    });
  }

  register(){
    if(this.user.categoryId == "")
        this.user.categoryId='1';
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

  onSelectedChanged(event){
      this.user.categoryId= event.value;
  }
}
