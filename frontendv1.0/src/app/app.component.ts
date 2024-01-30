import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { AuthUserService } from './service/authservices/auth-user.service';
import { PostService } from './service/postservices/post.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Blogger';

  constructor(private postService: PostService, private router: Router){}

  logout(): void {
    this.postService.logout();
    this.router.navigate(['/login']);
  }
}
