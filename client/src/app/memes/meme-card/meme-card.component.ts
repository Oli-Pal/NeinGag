import { Component, Input, OnInit } from '@angular/core';
import { ToastRef, ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';
import { PhotoEditorComponent } from '../photo-editor/photo-editor.component';
import { PhotosService } from 'src/app/_services/photos.service';


@Component({
  selector: 'app-meme-card',
  templateUrl: './meme-card.component.html',
  styleUrls: ['./meme-card.component.css']
})
export class MemeCardComponent implements OnInit {
  @Input() photos: Photo;
  @Input() member: Member;
  user: User;
  likes: number;

  //numberOfLikes: number = this.photos.likers;

    constructor(private accountService: AccountService,
       private memberService: MembersService,
        private toastr: ToastrService) {
          this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
            //debugger;
            this.user = user;
           // this.user.id = this.member.id;
           //debugger;
            //this.user.id = this.member.id;
          });
         }

  // likeButtonClick(){
  //   this.numberOfLikes++;
  // }
  // dislikeButtonClick(){
  //   this.numberOfLikes--;
  // }
        

  ngOnInit(): void {
    this.getLikes();
  }

  sendLike(photoId: number){
    this.memberService.sendLike(this.user.id, photoId).subscribe(data => {

      this.toastr.success('You have upvoted this meme')
      this.getLikes();
      
    }, error => {
      this.toastr.error("You already upvoted this meme");
    });
  }

  getLikes(){
    this.memberService.getNumberOfPhotoLikes(this.photos.id).subscribe((data) => {
      this.likes = data;
    });
  }

  // 	getNumberOfPhotoLikes(id: number){
  //     let totalLikes: number;
  //     this.memberService.getNumberOfPhotoLikes(id).subscribe(data => totalLikes)
    
  // } //memecardcomp
}
