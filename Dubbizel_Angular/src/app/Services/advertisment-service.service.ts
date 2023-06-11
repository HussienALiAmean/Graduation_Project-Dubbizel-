import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IAdvertisment } from '../Interfaces/IAdvertisment';

@Injectable({
  providedIn: 'root'
})
export class AdvertismentServiceService {

  _AdvertismentCatUrl="http://localhost:7189/api/Advertisment/CategoryID?CategoryID=";
  _AdvertismentSubCatUrl="http://localhost:7189/api/Advertisment/subCategoryID?subCategoryID=";

  constructor(private http:HttpClient) { }

  getAdsByCategoryID(catId:any):Observable<IAdvertisment[]>
  {
   return this.http.get<IAdvertisment[]>(this._AdvertismentCatUrl+catId).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }

  getAdsBySubCategoryID(SubCatID:any):Observable<IAdvertisment[]>
  {
   return this.http.get<IAdvertisment[]>(this._AdvertismentSubCatUrl+SubCatID).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
 
}
