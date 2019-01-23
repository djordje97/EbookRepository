import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/book/book.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  books
  searchModel={
    'input':'',
    'firstField':'title',
    'secondField':'title',
    'type':'simple',
    'operation':'and',
    'secondInput':''
  }
  booleanShow=false;
  logged
  constructor( private bookService:BookService) { }

  ngOnInit() {
    this.logged=JSON.parse(localStorage.getItem("logged"));
  }

  slecetFirstField(event){
    var field=event.target.value;
    this.searchModel.firstField=field
  }
  selectSearchType(event){
    var type=event.target.value;
    if(type === 'boolean')
      this.booleanShow=true;
    else
    this.booleanShow=false;
    this.searchModel.type=type;
  }

  slecetSecondField(event){
    var field=event.target.value;
    this.searchModel.secondField=field
  }

  selectOperation(event){
    var operation=event.target.value;
    this.searchModel.operation=operation;
  }

  search(){
    console.log(this.searchModel);
    if(this.searchModel.input === "")
      return;
    this.bookService.search(this.searchModel).subscribe(data=>{
      this.books=data;
      console.log(data);
    })
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
