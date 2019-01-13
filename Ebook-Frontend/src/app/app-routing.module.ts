import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { CategoryComponent } from './category/category.component';
import { SearchComponent } from './search/search.component';
import { UserManagmentComponent } from './user-managment/user-managment.component';
import { CategoryManagmentComponent } from './category-managment/category-managment.component';
import { BookManagmentComponent } from './book-managment/book-managment.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { AddBookComponent } from './add-book/add-book.component';

const routes: Routes = [
  {path: '', redirectTo: '/home' ,pathMatch: 'full'},
  {path:'home',component:HomeComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'category/:categoryId',component:CategoryComponent},
  {path:'search',component:SearchComponent},
  {path:'manage/users',component:UserManagmentComponent},
  {path:'manage/categories',component:CategoryManagmentComponent},
  {path:'manage/books',component:BookManagmentComponent},
  {path:'edit/user/:username',component:EditUserComponent},
  {path:'add/book',component:AddBookComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
