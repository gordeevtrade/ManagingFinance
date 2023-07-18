import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/models/sign-up-login/UserModel';
import { AuthApiService } from 'src/app/services/api/authentications/auth-api.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
 public registrationForm: FormGroup;

  constructor(private authService: AuthApiService, private router: Router, private formBuilder: FormBuilder) {
    this.registrationForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });

   }

 public registration(): void {
  if (this.registrationForm.valid) {
    const { email, password } = this.registrationForm.value;
    const registerModel = new UserModel();
    registerModel.email = email;
    registerModel.password = password;

    this.authService.register(registerModel).subscribe(response => {
      this.router.navigate(['/login']);
    alert(response.message);
    }, error => {
      alert(error.error);
    });
  }
  }

}
