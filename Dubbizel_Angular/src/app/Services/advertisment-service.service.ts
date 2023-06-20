import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IAdvertisment } from '../Interfaces/IAdvertisment';
import { IAdvertismentDetails } from '../Interface/AdvertismentDetails';
import { IadvertismetUser } from '../Interface/Advertisments\'sUser';

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


  getDetails(AdvertismentId:any):Observable<IAdvertismentDetails>{
    return this.http.get<IAdvertismentDetails>(`http://localhost:7189/api/Advertisment/Details/${AdvertismentId}`).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }

  getAdvertismentUser(userId:any):Observable<IadvertismetUser>{
    return this.http.get<IadvertismetUser>(`http://localhost:7189/api/Advertisment/Advertisment's User/${userId}`).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }

 
}
