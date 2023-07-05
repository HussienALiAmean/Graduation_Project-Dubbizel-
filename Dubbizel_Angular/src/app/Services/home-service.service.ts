import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Icategory } from '../Interface/ICategory';
import { IAdsHomePage } from '../Interface/IAdvertisment';

@Injectable({
  providedIn: 'root'
})
export class HomeServiceService {

  constructor(private http:HttpClient) { }

  getCategoriesWithSub():Observable<Icategory[]>{
    return this.http.get<Icategory[]>(`http://localhost:7189/api/Category/CategoriesWithSubcategoriesAndAdvertisment`);
  }
  getAdvertisments(userId:any):Observable<IAdsHomePage[]>{
    return this.http.get<IAdsHomePage[]>(`http://localhost:7189/api/Advertisment/GetAllAdsInHomePage/${userId}`);
  }
}
