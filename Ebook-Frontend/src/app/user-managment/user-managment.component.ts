import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { MatDialog, MatDialogConfig } from '@angular/material';
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
   const dialogConfig=new MatDialogConfig();
   dialogConfig.data={
     title:'Delete user',
     content:'Are you realy want to delete this user'
   };
   const dialogRef=this.dialog.open(DialogComponent,dialogConfig);
   dialogRef.afterClosed().subscribe(result =>{
    if(result == true)
      this.userService.deleteUser(username).subscribe(data=>{
        window.location.reload(true);
      });
   });
  }
}
