import { CanActivateFn, Router } from '@angular/router';
import { PostService } from '../service/postservices/post.service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (route, state) => {
  if (inject(PostService).isAuthenticated()) {
    return true;
  } else {
    inject(Router).navigate(['/login']);
    return false;
  }
};
