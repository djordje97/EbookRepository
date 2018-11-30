import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { MatDialog } from '@angular/material';
import { DialogComponent } from './dialog/dialog.component';

@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.css']
})
export class UserManagmentComponent implements OnInit {
  userList;
  constructor(private userService:UserService,public dialog:MatDialog) { }

  ngOnInit() {
    this.userService.getAllUsers().subscribe(data =>{
      console.log(data);
      this.userList=data;
    });
  }

  deleteUser(event){
    var username=event.target.name;
   const dialogRef=this.dialog.open(DialogComponent);
   dialogRef.afterClosed().subscribe(result =>{
    if(result == true)
      this.userService.deleteUser(username).subscribe(data=>{

      });
   });
  }
}
