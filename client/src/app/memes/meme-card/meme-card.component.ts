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
  @Input() member: Member;
  user: User;

  //numberOfLikes: number = this.photos.likers;

    constructor(private accountService: AccountService,
       private memberService: MembersService,
        private toastr: ToastrService) {
          this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
            this.user = user;
          });
         }
        

  ngOnInit(): void {}

  sendLike(photoId: number){
    this.memberService.sendLike(this.user.id, photoId).subscribe(() => {
      this.toastr.success('You have upvoted this meme');
      }, error => {
      this.toastr.error(error);
    });
  }
}