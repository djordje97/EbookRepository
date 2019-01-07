import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  url="http://localhost:5000/api/"
  constructor(private http:HttpClient) { }

  getAllLanguages(){
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
      return this.http.get(this.url+"language",{headers:httpOptions.header});
  }

}
