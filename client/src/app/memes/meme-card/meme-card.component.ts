import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-meme-card',
  templateUrl: './meme-card.component.html',
  styleUrls: ['./meme-card.component.css']
})
export class MemeCardComponent implements OnInit {
  @Input() photos: Photo;
    constructor(private memberService: MembersService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  sendLike(photoId: number){
    
    this.memberService.sendLike(this.user.id, photoId).subscribe(data => {

      this.toastr.success('You have upvoted this meme')
     // this.likeButtonClick();
      
    }, error => {
      this.toastr.error(error);
    });
  }
}
