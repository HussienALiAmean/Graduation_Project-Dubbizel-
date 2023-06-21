import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, defer, from, throwError } from 'rxjs';
import { IReview } from '../Interfaces/Review';

@Injectable({
  providedIn: 'root'
})
export class ReviewserviceService {

constructor(private http:HttpClient) { }

  AddReview(obj:any)
{
    return defer(() => from(this.http.post("http://localhost:7189/api/ReviewRoom",obj))
    .pipe(catchError((err) => {
      return throwError(() => err.message || "server error");
    })));
}
DeleteReview(Id:any)
{
  return defer(() => from( this.http.delete<IReview>(`http://localhost:7189/api/ReviewRoom/Id?Id=${Id}`)).pipe(catchError((err) => {
    return throwError(() => err.message || "server error");
  })));
}
EditReview(editreview:any):Observable<IReview>
{
  return this.http.put<IReview>("http://localhost:7189/api/ReviewRoom",editreview).pipe(catchError((err)=>{
    return throwError(()=>err.errorMessage || "server error")
  }))
}

// GetAllReviews():Observable<IReview[]>
// {
//   return this.http.get<IReview[]>(`http://localhost:7189/api/ReviewRoom`)
// }
// GetThreeReviews():Observable<IReview[]>
// {
//   return this.http.get<IReview[]>(`http://localhost:7189/api/ReviewRoom/display`)
// }


}