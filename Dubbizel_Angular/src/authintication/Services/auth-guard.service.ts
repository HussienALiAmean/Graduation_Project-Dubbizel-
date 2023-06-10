import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(private jwtHelper: JwtHelperService, private router: Router) { }
  
  canActivate() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)){
      //const decodeToken = this.jwtHelper.decodeToken(token);
      //if(decodeToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']=='Resturant')
      return true;
    }
   this.router.navigate(["/auth/ResturantLogin"]); 
    return false;
  }

}
