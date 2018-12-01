import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import {FormsModule} from '@angular/forms';
import { UserService } from './services/user/user.service';
import{HttpClientModule} from '@angular/common/http'
import { BookService } from './services/book/book.service';
import {BrowserAnimationsModule} from "@angular/platform-browser/animations"
import {MatButtonModule,MatInputModule,MatSelectModule,MatDialogModule} from '@angular/material';
import { CategoryComponent } from './category/category.component';
import { SearchComponent } from './search/search.component';
import { CategoryManagmentComponent } from './category-managment/category-managment.component';
import { UserManagmentComponent } from './user-managment/user-managment.component';
import { BookManagmentComponent } from './book-managment/book-managment.component';
import { DialogComponent } from './user-managment/dialog/dialog.component';
import { EditUserComponent } from './edit-user/edit-user.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent,
    CategoryComponent,
    SearchComponent,
    CategoryManagmentComponent,
    UserManagmentComponent,
    BookManagmentComponent,
    DialogComponent,
    EditUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatDialogModule,
  ],
  providers: [UserService,BookService],
  bootstrap: [AppComponent],
  entryComponents:[DialogComponent]
})
export class AppModule { }
