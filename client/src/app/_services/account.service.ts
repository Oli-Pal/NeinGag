import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable(); // $ observable

  constructor(private http: HttpClient) { }

  login(model: any){ // after , we specify what body our post will have
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user){
          localStorage.setItem('user', JSON.stringify(user)); //populating user inside localstorage in browser, changing object to string
          this.currentUserSource.next(user); //setting this to currentuser we get back from api
        }
      })
    );
  }

  setCurrentUser(user: User){
    this.currentUserSource.next(user);

  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
