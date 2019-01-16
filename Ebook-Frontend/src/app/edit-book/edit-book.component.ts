import { Component, OnInit, ViewChild } from '@angular/core';
import { BookService } from '../services/book/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../services/category/category.service';
import { LanguageService } from '../services/language/language.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { DialogComponent } from '../user-managment/dialog/dialog.component';
import { NgForOf } from '@angular/common';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.component.html',
  styleUrls: ['./edit-book.component.css']
})
export class EditBookComponent implements OnInit {

  indexUnit;
  categories;
  languages;
  keywords = {
    'word': ''
  };
  @ViewChild('f')ngform:NgForm;
  updatedArray=[];
  constructor(private bookService:BookService,private router:ActivatedRoute,private navigate:Router,private categoryService: CategoryService, private languageService: LanguageService,public dialog: MatDialog) { }

  ngOnInit() {
    this.categoryService.getAllCategories().subscribe(data => {
      this.categories = data;
    });
    this.languageService.getAllLanguages().subscribe(response => {
      this.languages = response;
    });
    let bookId=0;
    this.router.params.subscribe(param=>{
      bookId=param["ebookId"];
   });

   this.bookService.getById(bookId).subscribe(data =>{
     this.indexUnit=data;
     console.log(this.indexUnit);
   })
  }

  addKeyword() {

    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = {
      title: 'Add keyword',
      isKeywordAdd: true,
      keywords: this.keywords,
      isCategoryAdd: false
    }
    const dialogRef = this.dialog.open(DialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      if (result == false)
        return;
      this.keywords.word = result;
      this.indexUnit.keywords.push(this.keywords.word);
      this.keywords.word = '';
    });
  }

  deleteKeyword(event){
    var text=event.target.name;
    for (const element of this.indexUnit.keywords) {
      if (element !== text && element !== '') {
         this.updatedArray.push(element); 
      }
    }
    this.indexUnit.keywords=this.updatedArray;
    this.updatedArray=[];
    text="";
  }

edit(){
  this.bookService.update(this.indexUnit.filename,this.indexUnit).subscribe(data =>{
      this.navigate.navigate(["/"]);
  });
}
}
