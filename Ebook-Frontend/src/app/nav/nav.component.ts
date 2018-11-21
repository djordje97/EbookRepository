import { Component, OnInit } from '@angular/core';
import { RouterLink, Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  token=localStorage.getItem("token");
  constructor(private router:Router) { }

  ngOnInit() {
  }

  logout(){
    localStorage.clear();
    this.router.navigate(["/home"]);
  }
}
