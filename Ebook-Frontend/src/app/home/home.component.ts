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

  downloadBook(event){
    let filename=event.target.name;
    console.log(filename);
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
