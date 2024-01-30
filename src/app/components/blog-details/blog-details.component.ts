import { Component, OnInit } from '@angular/core';
import { BlogPost } from '../../models/blog-post.model';
import { ActivatedRoute, ActivatedRouteSnapshot, RouterModule } from '@angular/router';
import { DatePipe } from '@angular/common';
import { PostService } from '../../service/postservices/post.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-blog-details',
  standalone: true,
  imports: [DatePipe, RouterModule],
  templateUrl: './blog-details.component.html',
  styleUrl: './blog-details.component.css'
})
export class BlogDetailsComponent implements OnInit {
  blogPost: BlogPost;
  featuredImageUrl: SafeResourceUrl;

  constructor(private route: ActivatedRoute, private sanitizer: DomSanitizer, private postService: PostService) { }

  ngOnInit(): void {
    const postId = this.route.snapshot.paramMap.get('id');
    this.postService.getBlogPostById(postId).subscribe(blogPost => {
      this.blogPost = blogPost;

      this.featuredImageUrl = this.sanitizer.bypassSecurityTrustResourceUrl(blogPost.featuredImageUrl);
    });
  }
}