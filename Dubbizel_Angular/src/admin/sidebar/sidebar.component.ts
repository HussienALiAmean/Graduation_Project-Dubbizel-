import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  constructor(private jwtHelper: JwtHelperService,private router: Router) { }


  public logOut = () => {
    localStorage.removeItem("jwt");
    localStorage.removeItem("ApplicationUserId");
    localStorage.removeItem("UserName");
    this.router.navigate(["/admin/dashboardLogin"]);
  }

}
