import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { PhotosService } from 'src/app/_services/photos.service';

@Component({
  selector: 'app-meme-list',
  templateUrl: './meme-list.component.html',
  styleUrls: ['./meme-list.component.css']
})
export class MemeListComponent implements OnInit {
  members: Member[];
  photos: Photo[];

  constructor(private memberService: MembersService, private photosService: PhotosService) { }

  ngOnInit(): void {
    this.loadPhotos();
  }


  loadPhotos(){
    this.photosService.getPhotos().subscribe(photos => {
      this.photos = photos;
    });
  }


}
