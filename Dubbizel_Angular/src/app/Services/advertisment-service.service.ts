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

  _AdvertismentUrl="http://localhost:7189/api/Advertisment";

  constructor(private http:HttpClient) { }

  getAds():Observable<IAdvertisment[]>
  {
   return this.http.get<IAdvertisment[]>(this._AdvertismentUrl).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }
 
}
