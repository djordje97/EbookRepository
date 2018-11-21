import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
}
