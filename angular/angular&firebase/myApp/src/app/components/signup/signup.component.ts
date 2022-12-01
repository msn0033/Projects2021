import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Interface/user';
import { AuthService } from 'src/app/service/auth.service';
import { UserService } from 'src/app/service/user.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  erroremail: string
  constructor(private authSer: AuthService, private us: UserService, private route: Router) { }

  ngOnInit(): void {
  }

  OnSignup(form) {
    let data: User = form.value;
    this.authSer.signup(data.email, data.password).then(result => {
      this.erroremail = '',
        this.us.addNewUser(result.user.uid, data.name, data.address).then(() => { this.route.navigate(['/']) })

    }).catch(result => this.erroremail = result.message)
  }

}
