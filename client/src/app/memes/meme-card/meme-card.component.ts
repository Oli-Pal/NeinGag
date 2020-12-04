import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-meme-card',
  templateUrl: './meme-card.component.html',
  styleUrls: ['./meme-card.component.css']
})
export class MemeCardComponent implements OnInit {
  @Input() photos: Photo;
    constructor() { }

  ngOnInit(): void {
  }

}
