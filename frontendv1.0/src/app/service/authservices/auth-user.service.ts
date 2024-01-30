import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthUserService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private httpClient: HttpClient) { }

  registerUser(formData: any) {
    return this.httpClient.post(`${this.apiBaseUrl}/register`, formData);
  }

  loginUser(formData: any): Observable<any> {
    // TODO: implemented these things
    formData.twoFactorCode = 'string';
    formData.twoFactorRecoveryCode = 'string';

    return this.httpClient.post(`${this.apiBaseUrl}/login`, formData);
  }
}

