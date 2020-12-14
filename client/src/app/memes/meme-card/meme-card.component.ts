import { Component, Input, OnInit } from '@angular/core';
import { ToastRef, ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { AccountService } from 'src/app/_services/account.service';
import { take } from 'rxjs/operators';
import { User } from 'src/app/_models/user';


@Component({
  selector: 'app-meme-card',
  templateUrl: './meme-card.component.html',
  styleUrls: ['./meme-card.component.css']
})
export class MemeCardComponent implements OnInit {
  @Input() photos: Photo;
  member: Member;
  user: User;
  likes: number;
  dislikes: number;
    constructor(private accountService: AccountService,
       private memberService: MembersService,
        private toastr: ToastrService) {
          this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
            this.user = user;
          });
         }

  ngOnInit(): void {
    this.loadMember();
    this.getLikes();
    this.getDisLikes();
    
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

  


}
