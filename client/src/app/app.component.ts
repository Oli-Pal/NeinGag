import {  Component, ElementRef, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit  {
  users: any;

  constructor( private accountService: AccountService, private elementRef: ElementRef) {}

  ngOnInit(): void {
    this.setCurrentUser();
    this.elementRef.nativeElement.ownerDocument.body.style.backgroundColor = 'black';
  }

  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
  }


}
