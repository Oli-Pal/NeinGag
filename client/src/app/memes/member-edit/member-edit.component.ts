
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ToastRef, ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { AccountService } from 'src/app/_services/account.service';
import { PhotosService } from 'src/app/_services/photos.service';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';
import { Pagination } from 'src/app/_models/pagination';
import { Comment } from 'src/app/_models/comment';
import { NgForm } from '@angular/forms';
import { Like } from 'src/app/_models/like';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css'],
})
export class MemberEditComponent implements OnInit {
  member: Member;
  members: Member[];
  comments: Comment[];
  comment: Comment;
  user: User;
  photos: Photo[];
  liked: Like[];
  photo: Photo;
  pagination: Pagination;
  pageNumber = 1; 
  pageSize = 500; 
  likes: number;
  dislikes: number;
  contentOf: string;
  container: 'comments';
  toastr: any;
 
  constructor(private accountService: AccountService, private memberService: MembersService, private photosService: PhotosService,private route: ActivatedRoute) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
  }

  ngOnInit(): void {
    let id: string = this.route.snapshot.paramMap.get('id');
    this.photosService.getPhotoById(+id).subscribe(photos => {
      this.photo = photos;
      this.loadPhotos();
      this.loadMember();
      this.getUserComments();
      this.getAllLikes();
    
    });
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
    //this.getComments()
    this.getUserComments();
  }

  // getComments(){
  //   this.memberService.getComments(this.photo.id,this.pageNumber,this.pageSize).subscribe(response => {
  //     this.comments = response.result;
  //     this.pagination = response.pagination;
  //   })
  // }
  getUserComments(){
    this.memberService.getUserComments(this.user.id, this.pageNumber,this.pageSize).subscribe(response =>{
      this.comments = response.result;
      this.pagination = response.pagination;
    })
  }

  getAllLikes(){
    this.memberService.getUserLikes(this.user.id ,this.pageNumber,this.pageSize).subscribe(response =>{
      this.liked = response.result;
      this.pagination = response.pagination;
    })
  }
 
}
