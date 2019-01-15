import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BookService } from '../services/book/book.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  books;
  logged;
  constructor(private router:ActivatedRoute,private bookService:BookService) { }

  ngOnInit() {
    this.router.params.subscribe(param=>{
      this.bookService.getByCategory(param["categoryId"]).subscribe(data =>{
        this.books=data;
      });
   });

   this.logged=JSON.parse(localStorage.getItem("logged"));
  }

  downloadBook(event){
    let bookId=event.target.id;
    let filename=event.target.name;
    console.log(bookId);
    this.bookService.downloadBook(filename).subscribe(response =>{
      console.log(response.headers);
      console.log("oo je : "+filename)
      var url = window.URL.createObjectURL(response.body);
      var a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = url;
      a.download = filename;
      a.click();
      window.URL.revokeObjectURL(url);
      a.remove(); 
    });
  }
}
