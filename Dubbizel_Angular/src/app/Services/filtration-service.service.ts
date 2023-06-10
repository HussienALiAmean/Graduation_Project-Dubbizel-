import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class FiltrationServiceService {

  _CategoryUrl="http://localhost:7189/api/Category";

  constructor(private http:HttpClient) { }

  getCategories()//:Observable<><ICategory[]>
  {
   return this.http.get(this._CategoryUrl).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
  
}
