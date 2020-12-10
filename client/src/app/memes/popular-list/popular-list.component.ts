import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { PhotosService } from 'src/app/_services/photos.service';

@Component({
  selector: 'app-popular-list',
  templateUrl: './popular-list.component.html',
  styleUrls: ['./popular-list.component.css']
})
export class PopularListComponent implements OnInit {

  members: Member[];
  photos: Photo[];
  pagination: Pagination;
  pageNumber = 1; //chwilowka
  pageSize = 5; //chwilowka

  constructor(private memberService: MembersService, private photosService: PhotosService) { }

  ngOnInit(): void {
    this.loadPhotos();
  }


  loadPhotos(){
    this.photosService.getPhotos(this.pageNumber, this.pageSize)
    .subscribe(photos => {
      this.photos = photos.result;
      this.pagination = photos.pagination;
    });
  }

  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadPhotos();
  }
}
