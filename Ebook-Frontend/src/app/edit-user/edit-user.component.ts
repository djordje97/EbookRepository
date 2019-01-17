import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  user:any={

  };
  @ViewChild('f')ngform:NgForm;
  confirmPassword:any={

  };
  changePassword=false;
  dontMatchError=false;
  constructor(private userService:UserService,private router:ActivatedRoute,private navigate:Router) { }

  ngOnInit() {
    let username="";
    this.router.params.subscribe(param=>{
       username=param["username"];
    });
    this.userService.getUser(username).subscribe(data =>{
      this.user=data;
    });
   
  }

  onCheck(){
    this.changePassword=!this.changePassword;
  }
  matchPassword(event){
    console.log(this.confirmPassword);
  this.confirmPassword.password=event.target.value;
    if(this.user.password != this.confirmPassword.password)
      {
        this.dontMatchError=true;
        this.ngform.form.setErrors({'invalid':true});
      }
    else
    this.dontMatchError=false;
  }

  edit(){
    this.userService.editUser(this.user,this.user.username).subscribe(result=>{
       console.log(result);
       this.navigate.navigate(['/'])
    });
  }
}
