import { Photo } from './photo';

export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    age: number;
    nickname: string;
    created: Date;
    lastActive: Date;
    country: string;
    photos: Photo[];
  }
