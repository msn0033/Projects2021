import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  Ispoen: boolean = false;
  IsUser: boolean = false;

  constructor(private authSer: AuthService) {
  }

  ngOnInit(): void {
    this.authSer.IsUser.subscribe(result => {
      if (result) this.IsUser = true
      else this.IsUser = false
      this.authSer.UserId = result.uid;
    });
  }
  navbarshow() {
    this.Ispoen = !this.Ispoen;
  }
  LogOut() {
    this.authSer.SignOut();
  }

}
