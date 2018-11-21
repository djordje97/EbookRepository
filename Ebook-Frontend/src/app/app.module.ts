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
import {MatButtonModule,MatInputModule} from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatInputModule,
  ],
  providers: [UserService,BookService],
  bootstrap: [AppComponent]
})
export class AppModule { }
