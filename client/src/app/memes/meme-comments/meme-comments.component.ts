
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

@Component({
  selector: 'app-meme-comments',
  templateUrl: './meme-comments.component.html',
  styleUrls: ['./meme-comments.component.css']
})
export class MemeCommentsComponent implements OnInit {
  @ViewChild('messageForm') messageForm: NgForm;
  @Input() photos: Photo;
  member: Member;
  memb: Member[];
  @Input() comments: Comment[];
  pagination: Pagination;
  user: User;
  com: Comment[];
  likes: number;
  dislikes: number;
  contentOf: string;
  container: 'comments';
  pageNumber = 1;
  pageSize = 99;


  constructor(private accountService: AccountService,
    private memberService: MembersService,
     private toastr: ToastrService, private photosService: PhotosService,  private route: ActivatedRoute) {
       this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
         this.user = user;
       });
      

      }

      ngOnInit(): void {
       let id: string = this.route.snapshot.paramMap.get('id');
       this.photosService.getPhotoById(+id).subscribe(photos => {
         this.photos = photos;
         
         this.getLikes();
         this.getDisLikes();
         this.loadMember();
         this.getComments();
         this.loadOneMember();
       });
      }

      sendLike(photoId: number){
        this.memberService.sendLike(this.user.id, photoId).subscribe(data => {
          this.getLikes();
          this.getDisLikes();
        }, error => {
          this.toastr.error(error);
        });
      }
    
      sendDisLike(photoId: number){
        this.memberService.sendDisLike(this.user.id, photoId).subscribe(data => {
          this.getLikes();
          this.getDisLikes();
        }, error => {
          this.toastr.error(error);
        });
      }
    
      getLikes(){
        
        this.memberService.getNumberOfPhotoLikes(this.photos.id).subscribe((data) => {
          this.likes = data;
          
        });
      }
      getDisLikes(){
        
        this.memberService.getNumberOfPhotoDisLikes(this.photos.id).subscribe((data) => {
          this.dislikes = data;
        });
      }
    
      loadMember(){
        
        this.memberService.getMembers().subscribe((member) => {
          this.memb = member;
        });
      }

      getComments(){
        this.memberService.getComments(this.photos.id,this.pageNumber,this.pageSize).subscribe(response => {
          this.comments = response.result;
          this.pagination = response.pagination;
        })
      }

      addComment(){
        // debugger;
      this.memberService.addComment(this.user.id,this.photos.id, this.contentOf).subscribe(comment =>{
        this.comments.unshift(comment);
        this.messageForm.reset();
        this.getComments();
      });
    }

    deleteComment(id: number, commenterId: number) {
      if(this.user.id == commenterId){
        this.memberService.deleteComment(id, this.user.id).subscribe(() => {
          this.comments = this.comments.filter(x => x.id !== id);
        });
      }
    
  }

  loadOneMember(){
    this.memberService.getMember(this.photos.nickname.toLowerCase()).subscribe((member) => {
      this.member = member;
    });
  }
}
