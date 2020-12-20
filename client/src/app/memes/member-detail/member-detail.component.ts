import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Like } from 'src/app/_models/like';
import { Member } from 'src/app/_models/member';
import { Pagination } from 'src/app/_models/pagination';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { PhotosService } from 'src/app/_services/photos.service';
import { Comment } from 'src/app/_models/comment';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member;
  @Input() members: Member[];
  photos: Photo[];
  @Input() photoss: Photo;
  liked: Like[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 500;
  user: User;
  comments: Comment[];
  comment: Comment;
  commentsNoDuplicates: Comment[] = [];

  constructor(private memberService: MembersService, private route: ActivatedRoute,
     private photosService: PhotosService, private router: Router, private accountService: AccountService) { 
    
     
     }

  ngOnInit(): void {
    this.loadMember();
    this.loadMemberPhotos();
    
   
  }
  loadMember(){
    this.memberService.getMember(this.route.snapshot.paramMap.get('username'))
    .subscribe(member => {
      this.member = member;
      this.getUserComments();
      this.getAllLikes();
    });
  }
    loadMemberPhotos(){
      this.photosService.getMemberPhotos(this.route.snapshot.paramMap.get('username'))
      .subscribe(photos => {
        this.photos = photos;
        //odswiezanie memberow po wybraniu innego
        if(!localStorage.getItem('cos')) {
          localStorage.setItem('cos', 'no reload');
          location.reload();
        } else {
          localStorage.removeItem('cos');
        }
      });
  }


  
  getUserComments(){
    this.memberService.getUserComments(this.member.id, this.pageNumber, this.pageSize).subscribe(response =>{
      this.comments = response.result;
      this.getComments();
      this.pagination = response.pagination;
    });
  }

 

  getComments() {
    for(let i=0; i<this.comments.length; i++) {
        if(!this.commentsNoDuplicates.some(x => x.commentedPhotoId === this.comments[i].commentedPhotoId)) {
          this.commentsNoDuplicates.push(this.comments[i]);
        }
    }
  }
  getAllLikes(){
    this.memberService.getUserLikes(this.member.id, this.pageNumber, this.pageSize).subscribe(response =>{
      this.liked = response.result;
      this.pagination = response.pagination;
    });
  }
}
