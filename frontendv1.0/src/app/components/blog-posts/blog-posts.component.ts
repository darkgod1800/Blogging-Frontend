import { Component, OnInit } from '@angular/core';
import { PostService } from '../../service/postservices/post.service';
import { CommonModule, DatePipe } from '@angular/common';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-blog-posts',
  standalone: true,
  imports: [CommonModule, DatePipe],
  templateUrl: './blog-posts.component.html',
  styleUrl: './blog-posts.component.css'
})
export class BlogPostsComponent implements OnInit {
  blogPosts: any[] = [];

  constructor(private postService: PostService, private router: Router) { }

  ngOnInit(): void {
    this.loadBlogPosts();
  }

  loadBlogPosts(): void {
    this.postService.getBlogPosts().subscribe({
      next: (response) => {
        this.blogPosts = response;
      },
      error: (error) => {
        console.error('Error fetching blog posts:', error);
      }
    });
  }

  viewDetails(postId: string): void {
    this.router.navigate(['/blogposts', postId]);
  }
}