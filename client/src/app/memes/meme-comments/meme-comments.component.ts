import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-meme-comments',
  templateUrl: './meme-comments.component.html',
  styleUrls: ['./meme-comments.component.css']
})
export class MemeCommentsComponent implements OnInit {
  @Input() member: Member;
  constructor() { }

  ngOnInit(): void {
  }

}
