import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, tap } from 'rxjs';
import { JwtService } from './jwt.service';
import {UserModel} from "../../../models/sign-up-login/UserModel";
import {LoginResponse} from "../../../models/sign-up-login/LoginResponse";
import { ProfileService } from '../../profile/profile.service';
import {environment} from "../../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {

  private  _registrationUserUrl =  `${environment.apiUrl}/RegistrationUser`;
  private  _googleAuthUrl =  `${environment.apiUrl}/GoogleAccount`;

  constructor(private http: HttpClient,
  private jwtService: JwtService,
  private _profileService: ProfileService,
  ) { }

 public register(user: UserModel): Observable<any> {
    return this.http.post<UserModel>(this._registrationUserUrl+ '/register', user);
  }

  public login(model: UserModel): Observable<LoginResponse> {
    const url = `${this._registrationUserUrl}/login`;
    return this.http.post<LoginResponse>(url, model).pipe(
      tap(response => {
        this.jwtService.saveToken(response.token, response.expiresIn);
        this._profileService.setUserEmail(response.email);
      })
    );
  }

public getGoogleToken(authCode: string): Observable<any> {
  const url = `${this._googleAuthUrl}/callback?code=${authCode}`;
  return this.http.post<LoginResponse>(url, {}).pipe(
    tap(response => {
      this.jwtService.saveToken(response.token, response.expiresIn);
      this._profileService.setUserEmail(response.email);
    })
  );
}

  public logout() {
    this.jwtService.removeToken();
  }

 public isLoggedIn(): boolean {
    return this.jwtService.isTokenExpired();
  }

}
