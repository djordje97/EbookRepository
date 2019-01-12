import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/book/book.service';

@Component({
  selector: 'app-book-managment',
  templateUrl: './book-managment.component.html',
  styleUrls: ['./book-managment.component.css']
})
export class BookManagmentComponent implements OnInit {

  books;
  constructor(private bookService:BookService) { }

  ngOnInit() {
    this.bookService.getAllBooks().subscribe(response =>{
      console.log(response);
      this.books=response;
    });
  }

}
