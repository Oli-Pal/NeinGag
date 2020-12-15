
import { Component, Input, OnInit } from '@angular/core';
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

@Component({
  selector: 'app-meme-comments',
  templateUrl: './meme-comments.component.html',
  styleUrls: ['./meme-comments.component.css']
})
export class MemeCommentsComponent implements OnInit {
  @Input() photos: Photo;
  member: Member;
  comments: Comment[];
  pagination: Pagination;
  user: User;
  likes: number;
  dislikes: number;
  contentOf: string;
  container: 'comments';
  pageNumber = 1;
  pageSize = 99;
  //newComment: any = {};


  constructor(private accountService: AccountService,
    private memberService: MembersService,
     private toastr: ToastrService, private photosService: PhotosService,  private route: ActivatedRoute) {
       this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
         this.user = user;
       });
      

      }

      ngOnInit(): void {
       let id:string = this.route.snapshot.paramMap.get('id')
       this.photosService.getPhotoById(+id).subscribe(photos => {
         this.photos = photos;
         this.getLikes();
         this.getDisLikes();
         this.loadMember();
         this.getComments(this.photos.id)
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
        
        this.memberService.getMember(this.photos.nickname.toLowerCase()).subscribe((member) => {
          this.member = member;
        });
      }

      getComments(id: number){
        this.memberService.getComments(this.photos.id,this.pageNumber,this.pageSize).subscribe(response => {
          this.comments = response.result;
          this.pagination = response.pagination;
        })

      

      }

}
