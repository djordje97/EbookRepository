import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  url="http://localhost:5000/api/";
  constructor(private http:HttpClient) { }

  getCategories():any{
    return this.http.get(this.url+"category");
  }

  getAllCategories():any{
    return this.http.get(this.url+"category/all");
  }

  deleteCategory(categoryId):any{
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
      let finalUrl=this.url+"category/"+categoryId;
      console.log(finalUrl);
      return this.http.delete(finalUrl,{headers:httpOptions.header});
  }

  addCategory(category):any{
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
      let finalUrl=this.url+"category";
      return this.http.post(finalUrl,category,{headers:httpOptions.header});
  }
}
