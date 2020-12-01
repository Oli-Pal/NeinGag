import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;

  private currentUserSource = new ReplaySubject<User>(1);

  currentUser$ = this.currentUserSource.asObservable(); // $ observable

  constructor(private http: HttpClient) { }

  register(model: any){
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );

  }

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
