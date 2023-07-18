import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/models/sign-up-login/UserModel';
import { AuthApiService } from 'src/app/services/api/authentications/auth-api.service';
import {environment} from "../../../environments/environment";


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public loginForm: FormGroup;
  private googleOauthUrl = environment.googleOauthUrl;   
  private redirectUri =    environment.redirectUri;        
  private clientId =    environment.clientId;           
  private responseType = environment.responseType;    
  private scope = environment.scope; 


  constructor(private authService: AuthApiService,
    private router: Router,
    private formBuilder: FormBuilder,)
    {
      this.loginForm = this.formBuilder.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]]
      });}

  public  googleLogin() {
    const params = new HttpParams()
    .set('client_id', this.clientId)
    .set('redirect_uri', this.redirectUri)
    .set('response_type', this.responseType)
    .set('scope', this.scope)
   const authUrl = `${this.googleOauthUrl}?${params.toString()}`;
    window.open(authUrl);
  }

      public login(): void {
      if (this.loginForm.valid) {
        const { email, password } = this.loginForm.value;
        const loginModel = new UserModel();
        loginModel.email = email;
        loginModel.password = password;
        this.authService.login(loginModel).subscribe(response => {
        this.router.navigate(['/dashboard-layout']);
       alert("Вход успешно выполнен");
       }, error => {
        alert(error.error);
       });
   }
  }
}
