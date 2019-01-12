import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserManagmentComponent } from 'src/app/user-managment/user-managment.component';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }
  url="http://localhost:12621/api/"
  login(user):any{
 
    return this.http.post(this.url+"auth/login",user)
  }

  register(user):any{
    return this.http.post(this.url+"/user",user);
  }
  getUser(username):any{
      const tokenObject=JSON.parse(localStorage.getItem("token"));
    var head;
    if(tokenObject.token){
      head={
          "Authorization": "Bearer " + tokenObject.token,
          'Content-Type': 'application/json'
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };

   return this.http.get(this.url+"user/"+username,{headers:httpOptions.header});
  }
  getLogged(tokenObject):any{
    var head;
    if(tokenObject.token){
      head={
          "Authorization": "Bearer " + tokenObject.token,
          'Content-Type': 'application/json'
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };

      return this.http.get(this.url+"auth/logged",{headers:httpOptions.header});
  }

  getAllUsers():any{
    var head;
    var tokenObject=JSON.parse(localStorage.getItem("token"));
    if(tokenObject){
      head={
          "Authorization": "Bearer " +tokenObject.token,
          'Content-Type': 'application/json'
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };

      return this.http.get(this.url+"user",{headers:httpOptions.header});
  }

  deleteUser(username):any{
    var head;
    var tokenObject=JSON.parse(localStorage.getItem("token"));
    if(tokenObject){
      head={
          "Authorization": "Bearer " +tokenObject.token,
          'Content-Type': 'application/json'
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };
      let finalUrl=this.url+"user/"+username;
      console.log(finalUrl);
      return this.http.delete(finalUrl,{headers:httpOptions.header});
  }

  editUser(user,username):any{
    var head;
    var tokenObject=JSON.parse(localStorage.getItem("token"));
    if(tokenObject){
      head={
          "Authorization": "Bearer " +tokenObject.token,
          'Content-Type': 'application/json'
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };
      let finalUrl=this.url+"user/"+username;
      return this.http.put(finalUrl,user,{headers:httpOptions.header});
  }
}
