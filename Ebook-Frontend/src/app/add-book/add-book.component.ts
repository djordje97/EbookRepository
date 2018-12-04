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
  fileUploaded = false;
  pdfError = false;
  categories;
  languages;
  keywords = {
    'word': ''
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

  editKeyword(event) {
    var text = event.target.name;
    const dialogConfig = new MatDialogConfig();
    this.keywords.word = text;
    dialogConfig.data = {
      title: 'Edit keyword',
      isKeywordAdd: true,
      keywords: this.keywords,
      isCategoryAdd: false
    }
    const dialogRef = this.dialog.open(DialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => {
      if (result == false)
        return;
      this.keywords.word = result;
      console.log(this.indexUnit.keywords);
     for (const element of this.indexUnit.keywords) {
       if (element !== text) {
          this.updatedArray.push(element); 
       }
     }
     this.updatedArray.push(this.keywords.word);
     this.indexUnit.keywords=this.updatedArray;
      text="";
      this.updatedArray=[];
    });
  }
}
