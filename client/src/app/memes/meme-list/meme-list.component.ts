import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { PhotosService } from 'src/app/_services/photos.service';

@Component({
  selector: 'app-meme-list',
  templateUrl: './meme-list.component.html',
  styleUrls: ['./meme-list.component.css']
})
export class MemeListComponent implements OnInit {
  member: Member;
  photos: Photo[];
  pagination: Pagination;
  pageNumber = 1; 
  pageSize = 5; 

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
