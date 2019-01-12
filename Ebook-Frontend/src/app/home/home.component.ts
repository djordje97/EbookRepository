import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BookService } from '../services/book/book.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  books;
  logged;
  constructor(private bookService:BookService) { }

  ngOnInit() {
    this.bookService.getAllBooks().subscribe(response =>{
      console.log(response);
      this.books=response;
    });
    this.logged=JSON.parse(localStorage.getItem("logged"));
  }

}
