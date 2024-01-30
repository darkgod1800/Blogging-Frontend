import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthUserService } from '../../service/authservices/auth-user.service';
import { PostService } from '../../service/postservices/post.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm: FormGroup;
  showPassword: boolean = false;

  constructor(private fb: FormBuilder, private router: Router, private authService: AuthUserService, private postService: PostService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
  
  onSubmit() {
    if (this.loginForm.valid) {
      const formData = this.loginForm.value;

      this.authService.loginUser(formData).subscribe({
        next: (value) => {
          console.log('Login successful:', value);
          this.postService.setAccessToken(value.accessToken);
          this.router.navigate(['/blogposts']);
        },
        error: (error) => {
          console.error('Login failed:', error);
          this.router.navigate(['/register']);
        }
      });
    }
  }

  navigateToRegister() {
    this.router.navigate(['/register']);
  }
}

