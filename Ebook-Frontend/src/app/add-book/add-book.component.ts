import { Component, OnInit, ViewChild } from '@angular/core';
import { BookService } from '../services/book/book.service';
import { HttpEventType } from '@angular/common/http';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { CategoryService } from '../services/category/category.service';
import { LanguageService } from '../services/language/language.service';
import { DialogComponent } from '../user-managment/dialog/dialog.component';
import { element } from 'protractor';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-book',
  templateUrl: './add-book.component.html',
  styleUrls: ['./add-book.component.css']
})
export class AddBookComponent implements OnInit {
  fileToUpload: File = null;
  indexUnit;
  success=false;
  fileUploaded = false;
  pdfError = false;
  categories;
  languages;
  keywords = {
    'word': ''
  };
  viewModel={
    'file':null,
    'indexUnit':null
  };
  @ViewChild('f')ngform:NgForm;
  updatedArray=[];
  constructor(private bookService: BookService, private categoryService: CategoryService, private languageService: LanguageService, public dialog: MatDialog) { }

  ngOnInit() {
    if(this.fileToUpload == null)
    this.ngform.form.setErrors({'invalid':true});
    this.categoryService.getAllCategories().subscribe(data => {
      this.categories = data;
    });
    this.languageService.getAllLanguages().subscribe(response => {
      this.languages = response;
    });
  }

  fileSelect(event) {
    this.fileToUpload = event.target.files[0];
    if (this.fileToUpload.type != 'application/pdf') {
      event.target.value = null;
      this.pdfError = true;
      this.ngform.form.setErrors({'invalid':true});
    } else {
      this.pdfError = false;
      this.bookService.uploadBook(this.fileToUpload).subscribe(result => {
        this.indexUnit = result;
        this.fileUploaded = true;
      });
    }
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

  addBook(){
    console.log(this.indexUnit.filename);
      this.bookService.saveBook(this.indexUnit).subscribe(result =>{
            this.success=true;
            this.ngform.reset();
      });
  }
}
