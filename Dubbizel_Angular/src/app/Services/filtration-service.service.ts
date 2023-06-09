import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ISubCategoryFilter } from '../Interfaces/ISubCategoryFilter';

@Injectable({
  providedIn: 'root'
})
export class FiltrationServiceService {

  _SubCatFilterUrl="http://localhost:7189/api/SubCategory_Filter/subCategoryID?subCategoryID=";


  constructor(private http:HttpClient) { }

  getSubCategoryFilters(subCategoryID:any):Observable<ISubCategoryFilter[]>
  {
   return this.http.get<ISubCategoryFilter[]>(this._SubCatFilterUrl+subCategoryID).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }

  getSubCategoryFiltersWithIds(subCategoryID:any)
  {
   return this.http.get("http://localhost:7189/api/SubCategory_Filter/GetAllFiltersWithAndValuesID?subCategoryID="+subCategoryID).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
  
}
