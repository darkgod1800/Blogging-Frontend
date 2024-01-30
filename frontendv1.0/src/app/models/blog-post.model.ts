import { nanoid } from 'nanoid';

export class BlogPost {
    public id: string;
    public title: string;
    public shortDescription: string;
    public content: string;
    public featuredImageUrl: string;
    public userHandle: string;
    public publishedDate: Date;
    public author: string;
    public isVisible: boolean;
  }
  