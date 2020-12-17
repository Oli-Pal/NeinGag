import { Component, Input, OnInit } from '@angular/core';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { Pagination } from 'src/app/_models/pagination';
import { PhotosService } from 'src/app/_services/photos.service';


@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
})
export class MemberEditComponent implements OnInit {
  member: Member;
  user: User;
  photos: Photo[];
  pagination: Pagination;
  pageNumber = 1; 
  pageSize = 5; 
 
  constructor(private accountService: AccountService, private memberService: MembersService, private photosService: PhotosService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    this.loadMember();
    this.loadPhotos();
  }

  loadMember(){
    this.memberService.getMember(this.user.username).subscribe(member => {
      this.member = member;
    });
  }

  loadPhotos(){ // zamienic na loading tylko twoich
    this.photosService.getPhotos(this.pageNumber, this.pageSize)
    .subscribe(photos => {
      this.photos = photos.result;
      this.pagination = photos.pagination;
    });
  }

  //dodac klase z polubionymi
  //dodac z zakomentowanymi

  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadPhotos();
  }
 
}
