import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model: any = {};
  registerMode = false;
  constructor(public accountService: AccountService, private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
  
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  login(){
   this.accountService.login(this.model).subscribe(response => {
     this.router.navigateByUrl('/memes');
   });
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
    
  }

  getCurrentUser(){
    this.accountService.currentUser$.subscribe(user => {
    }, error => {
        console.log(error);
    });
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event;
  }

}
