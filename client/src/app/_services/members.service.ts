import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) { }

  getMembers() {
    if (this.members.length > 0) { return of(this.members); }
    return this.http.get<Member[]>(this.baseUrl + 'users').pipe(
      map(members => {
        this.members = members;
        return members;
      })
    )
  }

  getMember(username: string) {
    const member = this.members.find(x => x.username === username);
    if (member !== undefined) { return of(member); }
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  getMemberById(id: number) {
    const member = this.members.find(x => x.id === id);
    if (member !== undefined) { return of(member); }
    return this.http.get<Member>(this.baseUrl + 'users/' + id);
  }

  setDescription(model: any) {
    return this.http.post(this.baseUrl + 'add-photo/completed', model).pipe(
      map(() => {
        localStorage.setItem('description', JSON.stringify(model))
        if (model != null) {
          console.log('ddddddd');
        }
      })
    );
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId);
  }

  sendLike(id: number, photoId: number) {
    return this.http.post(this.baseUrl + 'users/' + id + '/like/' + photoId, {})
  }

  sendDisLike(id: number, photoId: number) {
    return this.http.post(this.baseUrl + 'users/' + id + '/dislike/' + photoId, {})
  }

  getNumberOfPhotoLikes(id: number): Observable<number> {
    return this.http.get<number>(this.baseUrl + 'users/' + id + '/likes/', { observe: 'response' }).pipe(map((response) => {
      return response.body;
    }))
  }

  getNumberOfPhotoDisLikes(id: number): Observable<number> {
    return this.http.get<number>(this.baseUrl + 'users/' + id + '/dislikes/', { observe: 'response' }).pipe(map((response) => {
      return response.body;
    }))
  }
}
