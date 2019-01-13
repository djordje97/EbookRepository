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
  constructor(private router:ActivatedRoute,private bookService:BookService) { }

  ngOnInit() {
    let categoryId=0;
    this.router.params.subscribe(param=>{
      categoryId=param["categoryId"];
   });
   console.log(categoryId);

   this.bookService.getByCategory(categoryId).subscribe(data =>{
     this.books=data;
   })
  }

}
