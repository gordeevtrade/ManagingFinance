import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private userEmail: string = '';

  constructor() { }

 public setUserEmail(email: string): void {
    this.userEmail = email;
  }

 public getUserEmail(): string {
    return this.userEmail;
  }
}
