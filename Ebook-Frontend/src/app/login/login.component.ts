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
  username:string;
  password:string;
  ngOnInit() {
  }

  login(){
    if(this.password != "" && this.username!= "")
    {
      this.userService.login(this.username,this.password).subscribe(response =>{
        localStorage.setItem("token",JSON.stringify(response));
        this.router.navigate(['/home']);
      });
    }
  }
}
