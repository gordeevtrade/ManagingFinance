import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { AuthApiService } from 'src/app/services/api/authentications/auth-api.service';

@Component({
  selector: 'callback',
  template: '',
  styleUrls: ['./callback.component.css']
})
export class CallbackComponent {
  constructor(private route: ActivatedRoute, private authApiService : AuthApiService,
  private router: Router) {

    this.route.queryParams.subscribe(params => {
      const authCode = params['code'];
      if (authCode) {
     this.authApiService.getGoogleToken(authCode).subscribe(
      token => {
        console.log('Token:', token);  
        this.router.navigate(['/dashboard-layout']);
      },
      error => {

      }
    );
      }
    });
  }


}

