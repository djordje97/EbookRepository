import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }
  url="http://localhost:12621/api/"
  login(user){
 
    return this.http.post(this.url+"auth/login",user)
  }

  register(user){
    return this.http.post(this.url+"/user",user);
  }
  getUser(username){
   return this.http.get(this.url+"user/"+username);
  }
}
