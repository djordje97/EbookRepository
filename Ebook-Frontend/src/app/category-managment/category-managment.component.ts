import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category/category.service';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { DialogComponent } from '../user-managment/dialog/dialog.component';

@Component({
  selector: 'app-category-managment',
  templateUrl: './category-managment.component.html',
  styleUrls: ['./category-managment.component.css']
})
export class CategoryManagmentComponent implements OnInit {

  categories:Array<any>;
  category={
    'name':''
  };
  constructor(private categoryService:CategoryService,public dialog:MatDialog) { }

  ngOnInit() {
    this.categoryService.getCategories().subscribe(data =>{
      console.log(data);
        this.categories=data;
    });
  }

  addCategory(){
    const dialogConfig=new MatDialogConfig();
    dialogConfig.data={
      title:'Add category',
      isCategoryAdd:true,
      category:this.category
    };
    const dialogRef=this.dialog.open(DialogComponent,dialogConfig);
    dialogRef.afterClosed().subscribe(result =>{
      if(result == false)
        return;
      this.category.name=result;
     this.categoryService.addCategory(this.category).subscribe(data =>{
        this.categories.push(data);
     });
    });
  }
  deleteCategory(event){
    var categoryId=event.target.name;
    const dialogConfig=new MatDialogConfig();
    dialogConfig.data={
      title:'Delete category',
      content:'Are you really want to delete this category'
    };
    const dialogRef=this.dialog.open(DialogComponent,dialogConfig);
    dialogRef.afterClosed().subscribe(result =>{
     if(result == true)
       this.categoryService.deleteCategory(categoryId).subscribe(data =>{
         window.location.reload(true);
       });
    });
  }
}
