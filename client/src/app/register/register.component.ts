import { Component, Input, OnInit } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() usersFromHomeComponent: any;
  model: any = {};
  registerMode = false;
  constructor() { }
  
  ngOnInit(): void {
  
  }


  register(){
    console.log(this.model);
  }

  cancel(){

    console.log('Cancelled');
  }

}
