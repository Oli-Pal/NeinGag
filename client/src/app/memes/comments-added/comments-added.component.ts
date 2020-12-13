import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';
import { MembersService } from 'src/app/_services/members.service';


@Component({
  selector: 'app-comments-added',
  templateUrl: './comments-added.component.html',
  styleUrls: ['./comments-added.component.css']
})
export class CommentsAddedComponent implements OnInit {


  
  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    
  }

 
}
