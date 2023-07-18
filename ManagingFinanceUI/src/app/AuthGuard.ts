import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import {AuthApiService} from "./services/api/authentications/auth-api.service";
import {JwtService} from "./services/api/authentications/jwt.service";


@Injectable({
  providedIn: 'root'
})

export class AuthGuard implements CanActivate {

  constructor(private authService: AuthApiService, private router: Router, private jwtService: JwtService) { }

 public canActivate(): boolean {
    return this.checkTokenExpiration();
  }

 public canActivateChild(): boolean {
    return this.checkTokenExpiration();
  }

  private checkTokenExpiration(): boolean {
    console.log("Is Logged In:", this.authService.isLoggedIn());
    if (!this.jwtService.isTokenExpired()) {
      alert('Сессия истекла. Пожалуйста, выполните повторный вход в систему.');
      this.router.navigate(['/login']);
      return false;
    }
    return true;
  }


}
