import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  Destroy: Subscription;
  constructor(private authSer: AuthService, private router: Router) {
  }


  ngOnInit(): void {
    this.Destroy = this.authSer.IsUser.subscribe(result => {
      if (result) this.router.navigate(['/'])
    })
  }
  OnSignIn(form) {
    this.authSer.SignIn(form).then(result => this.router.navigate(['/']))
  }
  ngOnDestroy(): void {
    this.Destroy.unsubscribe();
  }

}
