import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/member';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Comment } from '../_models/comment';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Photo } from '../_models/photo';
import { Like } from '../_models/like';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  comments: Comment[] = [];
  
  

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

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'photo/delete-photo/' + photoId);
  }

  sendLike(id: number, photoId: number) {
    return this.http.post(this.baseUrl + 'users/' + id + '/like/' + photoId, {});
  }

  sendDisLike(id: number, photoId: number) {
    return this.http.post(this.baseUrl + 'users/' + id + '/dislike/' + photoId, {});
  }

  addComment(id: number, photoId: number, contentOf: string) {
    const form = new FormData();
    form.append("contentOf", contentOf);
    return this.http.post<Comment>(this.baseUrl + 'comment/' + id + '/' + photoId, form);
  }

  deleteComment(id: number, commenterId: number) {
    return this.http.delete(this.baseUrl + 'comment/delete/' + id + '/' + commenterId);
  }

  getComments(id: number, pageNumber, pageSize) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    //params = params.append('Container', container);
    return getPaginatedResult<Comment[]>(this.baseUrl + 'comment/' + id, params, this.http);
    
  }

  getUserComments(id: number, pageNumber, pageSize) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    //params = params.append('Container', container);
    return getPaginatedResult<Comment[]>(this.baseUrl + 'comment/byUser/' + id, params, this.http);
    
  }

  getUserLikes(id: number, pageNumber, pageSize) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    //params = params.append('Container', container);
    return getPaginatedResult<Like[]>(this.baseUrl + 'users/userlikes/' + id, params, this.http);
    
  }

  getNumberOfPhotoLikes(id: number): Observable<number> {
    return this.http.get<number>(this.baseUrl + 'users/' + id + '/likes/', { observe: 'response' })
    .pipe(map((response) => {
      return response.body;
    }));
  }

  getNumberOfPhotoDisLikes(id: number): Observable<number> {
    return this.http.get<number>(this.baseUrl + 'users/' + id + '/dislikes/', { observe: 'response' }).pipe(map((response) => {
      return response.body;
    }));
  }
  

  sendMoney(amount: string, currency: string, source: string, receiptEmail: string, cardNumber: string, date: string, cvc: string, region: string) {
    const form = new FormData();
    form.append("amount", amount);
    form.append("currency", currency);
    form.append("source", source);
    form.append("receiptEmail", receiptEmail);
    form.append("cardNumber", cardNumber);
    form.append("date", date);
    form.append("cvc", cvc);
    form.append("region", region);
    return this.http.post(this.baseUrl + 'charges/', form)
      
  }


  //[HttpPost("add-coins/{amount}/{id}")]
  //public async Task<ActionResult<MemberDto>> AddCoins(int amount, int id)

  updateAmount(amount: number, id: number){
    return this.http.post(this.baseUrl + "users/add-coins/" + amount + "/" + id, {});
  }
}
