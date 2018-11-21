import { Component, OnInit } from '@angular/core';
import { RouterLink, Router } from '@angular/router';
import { CategoryService } from '../services/category/category.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  token=localStorage.getItem("token");
  categories;
  constructor(private router:Router,private categoryService:CategoryService) { }

  ngOnInit() {
    this.categoryService.getAllCategories().subscribe(response =>{
        this.categories=response;
    });
  }

  logout(){
    localStorage.clear();
    this.router.navigate(["/home"]);
  }
}
