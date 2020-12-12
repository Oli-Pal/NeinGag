import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';
import { PhotosService } from 'src/app/_services/photos.service';

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
  constructor(private memberService: MembersService, private route: ActivatedRoute,
     private photosService: PhotosService, private router: Router) { }

  ngOnInit(): void {
    this.loadMember();
    this.loadMemberPhotos();
  }
  loadMember(){
    this.memberService.getMember(this.route.snapshot.paramMap.get('username'))
    .subscribe(member => {
      this.member = member;
      
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

}
