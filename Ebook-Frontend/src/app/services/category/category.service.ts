import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  url="http://localhost:12621/api/";
  constructor(private http:HttpClient) { }

  getAllCategories(){
    return this.http.get(this.url+"category");
  }
}
