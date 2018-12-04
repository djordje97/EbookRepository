import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

  title;
  isCategoryAdd
  content;
  data;
  isKeywordAdd;
  constructor( @Inject(MAT_DIALOG_DATA) dialogData) {
    this.data=dialogData;
    console.log(this.data);
   }

  
  ngOnInit() {

  }

}
