import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-meme-card',
  templateUrl: './meme-card.component.html',
  styleUrls: ['./meme-card.component.css']
})
export class MemeCardComponent implements OnInit {
  @Input() member: Member;
    constructor() { }

  ngOnInit(): void {
  }

}
