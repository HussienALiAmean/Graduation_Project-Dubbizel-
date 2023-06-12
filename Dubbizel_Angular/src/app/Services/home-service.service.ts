import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Icategory } from '../Interface/ICategory';

@Injectable({
  providedIn: 'root'
})
export class HomeServiceService {

  constructor(private http:HttpClient) { }

  getCategoriesWithSubAndAds():Observable<Icategory[]>{
    return this.http.get<Icategory[]>(`http://localhost:7189/api/Category/CategoriesWithSubcategoriesAndAdvertisment`);
  }
}
