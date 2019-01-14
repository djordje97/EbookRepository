import { Component, OnInit } from '@angular/core';
import { BookService } from '../services/book/book.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  searchModel={
    'input':'',
    'firstField':'title',
    'secondField':'title',
    'type':'simple',
    'operation':'and'
  }
  booleanShow=false;
  constructor( private bookService:BookService) { }

  ngOnInit() {
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
    this.bookService.search(this.searchModel).subscribe(data=>{
      alert("Success");
    })
  }
} 
