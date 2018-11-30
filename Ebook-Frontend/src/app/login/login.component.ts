import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private userService:UserService,private router:Router) { }
user={
  'username':'',
  'password':''
};
someError=false;
message=false;

  ngOnInit() {
  }

  login(){
    if(this.user.username != "" && this.user.password!= "")
    {
      this.userService.login(this.user).subscribe(response =>{
        this.userService.getLogged(response).subscribe(data =>{
          localStorage.setItem("logged",JSON.stringify(data));
          localStorage.setItem("token",JSON.stringify(response),);
        });
        
        setTimeout(()=>{
          this.router.navigate(['/home']);
        },1000);
      },error =>{
        this.someError=true;
      });
    }
    else {
      this.message=true
    }
  }
focus(){
  this.someError=false;
  this.message=false;
}
}
