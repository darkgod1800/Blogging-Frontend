import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { BlogPost } from '../../models/blog-post.model';
import { BlogAdd } from '../../models/blog-add.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private apiBaseUrl = environment.apiBaseUrl;
  private accessToken: string | null = null;

  constructor(private http: HttpClient) { }

  setAccessToken(token: string): void {
    this.accessToken = token;
  }

  getAccessToken(): string | null {
    return this.accessToken;
  }

  isAuthenticated(): boolean {
    // Check if the user is authenticated based on your criteria (e.g., token existence)
    return !!this.accessToken;
  }

  logout(): void {
    this.accessToken = null;
  }
 
  private getHeaders(): HttpHeaders {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.accessToken}`
    });
    return headers;
  } 

  getBlogPosts(): Observable<BlogPost[]> {
    const headers = this.getHeaders();
    return this.http.get<BlogPost[]>(`${this.apiBaseUrl}/api/BlogPost`, { headers });
  }

  getBlogPostById(id: string): Observable<BlogPost> {
    const headers = this.getHeaders();
    return this.http.get<BlogPost>(`${this.apiBaseUrl}/api/BlogPost/${id}`, { headers });
  }

  addBlogPost(blogAdd: BlogAdd): Observable<BlogPost> {
    const headers = this.getHeaders();
    return this.http.post<BlogPost>(`${this.apiBaseUrl}/api/BlogPost`, blogAdd, { headers });
  }

  uploadImage(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post(`${this.apiBaseUrl}/api/image`, formData);
  }

  getFullImageUrl(filePath: string): string {
    const baseUrl = this.apiBaseUrl;
    const relativePath = filePath.replace('file:///', '').replace(/\\/g, '/');
    const filename = relativePath.substring(relativePath.lastIndexOf('/') + 1);
    return `${baseUrl}/static/images/${filename}`;
  }
}
