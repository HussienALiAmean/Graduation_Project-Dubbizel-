import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IFavourite } from '../Interface/IFavorite';
import { IGetAllFavorite } from '../Interface/GetIFavorite';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {

  constructor(private http:HttpClient) { }

  AddFavorite(AdsFavourite:any):Observable<IFavourite>{
    return this.http.post<IFavourite>(`http://localhost:5115/api/Favorite/AddFavorite`,AdsFavourite);
  }
  DeleteFavorite(AdsId:any,userId:any):Observable<IFavourite>{
    return this.http.delete<IFavourite>(`http://localhost:5115/api/Favorite/DeleteFavourite/${AdsId}/${userId}`);
  }
  getAllFavorite(userId:any):Observable<IGetAllFavorite>{
    return this.http.get<IGetAllFavorite>(`http://localhost:5115/api/Favorite/GetAllFavorite/${userId}`);
  }
}
