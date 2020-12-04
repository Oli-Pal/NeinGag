import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Member } from '../_models/member';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Photo } from '../_models/photo';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  paginatedResult: PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  constructor(private http: HttpClient) { }

  getMembers(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();
    if(page !== null && itemsPerPage !== null){
        params = params.append('pageNumber', page.toString());
        params = params.append('pageSize', itemsPerPage.toString());
    } //pierdoly od pagination

   // if (this.members.length > 0) { return of(this.members); }
    return this.http.get<Member[]>(this.baseUrl + 'users', {observe: 'response', params}).pipe(
      // map(members => {
      //   this.members = members;
      //   return members;
      // })

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination')!== null){
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })
    )
  }

  getMember(username: string) {
    const member = this.members.find(x => x.username === username);
    if (member !== undefined) { return of(member); }
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  setDescription(model: any){
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
}
