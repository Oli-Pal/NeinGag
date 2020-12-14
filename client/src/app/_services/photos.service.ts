import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root'
})
export class PhotosService {
  baseUrl = environment.apiUrl;
  photos: Photo[] = [];
  paginatedResult: PaginatedResult<Photo[]> = new PaginatedResult<Photo[]>();

  constructor(private http: HttpClient) { }

  getPhotos(page?: number, itemsPerPage?:number){
    let params = new HttpParams();
    if(page !== null && itemsPerPage !== null){
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Photo[]>(this.baseUrl + 'users/photos', {observe: 'response', params}).pipe(
        map(response =>{
          this.paginatedResult.result = response.body;
          if(response.headers.get('Pagination') !== null){
            this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return this.paginatedResult;
        })
    );
  }

  getPopularPhotos(page?: number, itemsPerPage?:number){
    let params = new HttpParams();
    if(page !== null && itemsPerPage !== null){
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Photo[]>(this.baseUrl + 'users/popular-photos', {observe: 'response', params}).pipe(
        map(response =>{
          this.paginatedResult.result = response.body;
          if(response.headers.get('Pagination') !== null){
            this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return this.paginatedResult;
        })
    );
  }

  getMemberPhotos(username: string) {
    if (this.photos.length > 0) { return of(this.photos); }
    return this.http.get<Photo[]>(this.baseUrl + 'users/photos/' + username).pipe(
      map(photos => {
        this.photos = photos;
        return photos;
      })
    );
  }

  

  getPhotoById(id: number) {
    const photos = this.photos.find(x => x.id === id);
    if (photos !== undefined) { return of(photos); }
    return this.http.get<Photo>(this.baseUrl + 'users/get-photo/' + id);
  }
}
