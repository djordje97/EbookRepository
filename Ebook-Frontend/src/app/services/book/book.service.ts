import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookService {
  
   url="http://localhost:12621/api/";
  constructor(private http:HttpClient){ }
 
  
  
  getAllBooks():Observable<any>{
   return this.http.get(this.url+"ebook");
  }

  uploadBook(file:File):any{
    var head;
    var tokenObject=JSON.parse(localStorage.getItem("token"));
    if(tokenObject){
      head={
          "Authorization": "Bearer " +tokenObject.token,
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };
     let formData=new FormData();
     formData.append(file.name,file);
     return this.http.post(this.url+"ebook/upload",formData,{headers:httpOptions.header,reportProgress:true});
  }

  saveBook(indexUnit){
    var head;
    var tokenObject=JSON.parse(localStorage.getItem("token"));
    if(tokenObject){
      head={
          "Authorization": "Bearer " +tokenObject.token,
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };

      return this.http.post(this.url+"ebook",indexUnit,{headers:httpOptions.header});
  }

  indexBook(vievModel){
    var head;
    var tokenObject=JSON.parse(localStorage.getItem("token"));
    if(tokenObject){
      head={
          "Authorization": "Bearer " +tokenObject.token,
        };
      }else{
          head={
              'Content-Type': 'application/json'
          };
      }
     let  httpOptions= {
          header: new  HttpHeaders(head)
      };
    
      return this.http.post(this.url+"ebook/save",vievModel,{headers:httpOptions.header});
  }
}
