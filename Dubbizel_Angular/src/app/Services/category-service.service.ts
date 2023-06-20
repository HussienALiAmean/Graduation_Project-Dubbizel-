import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ICategory } from '../Interfaces/ICategory';

@Injectable({
  providedIn: 'root'
})
export class CategoryServiceService {


  _CategoryUrl="http://localhost:7189/api/Category";

  _CategoryUrl2="http://localhost:7189/api/Category/categoryId?categoryId=2"; 


  constructor(private http:HttpClient) { }

  getCategories():Observable<ICategory[]>
  {
   return this.http.get<ICategory[]>(this._CategoryUrl).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
  getAllofsubCategories(id:any):Observable<ICategory[]>
  {
    return this.http.get<ICategory[]>(this._CategoryUrl2).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
 

}
