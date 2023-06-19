import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { tokenGetter } from 'src/app/app.module';

@Injectable({
  providedIn: 'root'
})
export class EnrollService {

  _EmailUrl:string="http://localhost:5115/api/User/GetEmail";
  _EmailAndPasswordUrlRegister:string="http://localhost:5115/api/User/RegisterWithEmailAndPass";
  _EmailAndPasswordUrlLogin:string="http://localhost:5115/api/User/LoginWithEmailAndPass";

  headers?: HttpHeaders;

  constructor(private http:HttpClient) { }

  PostEmail(user:any)
  {
   return this.http.post(this._EmailUrl,user).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
 

  RegisterEmailAndPassword(user:any)
  {
   return this.http.post(this._EmailAndPasswordUrlRegister,user).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }


  LoginEmailAndPassword(user:any)
  {
   return this.http.post(this._EmailAndPasswordUrlLogin,user).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
 




  // TestToken()
  // {

  //   console.log(tokenGetter() as string)
  //     //httpOptions
  //     const headers = new HttpHeaders({
  //       'Content-Type': 'application/json',
  //       Authorization: `Bearer ${tokenGetter() as string}`,
  //     });
  //     //,{headers:headers}
  //       return this.http.get("http://localhost:59638/api/ResturantAccount/Index").pipe(catchError((err: any) => {
  //         return throwError(() => err.message || "server error");
  //         }));
  // }



}
