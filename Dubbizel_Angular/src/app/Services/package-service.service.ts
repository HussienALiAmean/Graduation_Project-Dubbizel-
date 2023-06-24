import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PackageServiceService {

  _PackagesUrl="http://localhost:7189/api/Package/GetAllPackageBySubCategoryID?SubCategoryID=";
  _PayPackagesUrl="http://localhost:7189/api/Package/PostApplicationUser_Package";



  constructor(private http:HttpClient) { }

  getPackages(subCategoryID:any)
  {
   return this.http.get(this._PackagesUrl+subCategoryID).pipe(catchError((err: any) => {
    return throwError(() => err.message || "server error");
    }));
  }

  Pay(PackageAppUser:any)
  {
    return this.http.post(this._PayPackagesUrl,PackageAppUser).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      })); 
  }
 
}
