import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { profile } from '../Interfaces/profile';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
//_url:string="http://localhost:7189/api/Profile/id?id=1"
  constructor(private http:HttpClient) { }

  editProfile(id:any,editform:any):Observable<profile>
  {
  return this.http.put<profile>(`http://localhost:7189/api/Profile/editProfile/${id}`,editform).pipe(catchError((err)=>{
    return throwError(()=>err.errorMessage || "server error")
  }))
  }

  GetProfile(id:any):Observable<profile>{
    return this.http.get<profile>(`http://localhost:7189/api/Profile/getProfile/${id}`).pipe(catchError((err)=>{
      return throwError(()=>err.errorMessage || "server error")
    }))
  }

  DeleteProfile(id:any){
    return this.http.delete<profile>(`http://localhost:7189/api/Profile/deleteProfile/${id}`).pipe(catchError((err)=>{
      return throwError(()=>err.errorMessage || "server error")
    }))
  }
}