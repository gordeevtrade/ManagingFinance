import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { JwtService } from 'src/app/services/api/authentications/jwt.service';


@Component({
  selector: 'app-logout',
  template: '',
})
export class LogoutComponent implements OnInit {

  constructor(private router: Router, private jwtService: JwtService) { }

  ngOnInit(): void {
    this.jwtService.removeToken();
    this.router.navigate(['/login']);
  }

}
