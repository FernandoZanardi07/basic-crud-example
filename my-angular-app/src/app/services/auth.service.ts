import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private tokenKey = 'authToken';

  constructor() { }

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    console.log('Token:', localStorage.getItem(this.tokenKey));
    return localStorage.getItem(this.tokenKey);
  }

  clearToken(): void {
    localStorage.removeItem(this.tokenKey);
  }

  getAuthenticationStatus(): boolean {
    return this.getToken() !== null;
  }

  logout(): void {
    this.clearToken();
  }
}