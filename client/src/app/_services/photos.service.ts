import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Photo } from '../_models/photo';

@Injectable({
  providedIn: 'root'
})
export class PhotosService {
  baseUrl = environment.apiUrl;
  photos: Photo[] = [];

  constructor(private http: HttpClient) { }

  getPhotos(){
    if (this.photos.length > 0) {return of(this.photos);}
    return this.http.get<Photo[]>(this.baseUrl + 'users/photos').pipe(
      map(photos => {
        this.photos = photos;
        return photos;
      })
    );
  }
}
