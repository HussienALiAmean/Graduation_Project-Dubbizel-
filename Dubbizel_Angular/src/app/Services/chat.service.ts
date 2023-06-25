import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IUser } from '../Interfaces/IUser';
import { IChatData } from '../Interfaces/IChatData';
@Injectable({
  providedIn: 'root'
})
export class ChatService {

  public top = 5;
  public skip = 0;
  private apiUrl = "http://localhost:7189/api/Chat/GetChatUsers?id=2946b9f6-35f7-4e2f-8c2a-7a0ab10885db" //'http://localhost:7189/api/User'
  private apiUrl3 = "http://localhost:7189/api/Chat/GetMessages?sender=2946b9f6-35f7-4e2f-8c2a-7a0ab10885db&reciver=99ba0e2f-a547-44ae-b85e-04b36dbeff4c" //'http://localhost:7189/api/User'
  private apiUrl2 = "http://localhost:7189/api/Chat/AddMessage" //'http://localhost:7189/api/User'
  private apiUrldelete = "http://localhost:7189/api/Chat" //'http://localhost:7189/api/User'
  constructor(private http:HttpClient) { }

  GetUsers():Observable<IUser[]>{
    console.log('reach')

    return this.http.get<IUser[]>(this.apiUrl).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }

  AddMessage(data:any){
    return this.http.post(this.apiUrl2,data).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }
  getChat(){
    console.log(this.top)
    console.log(this.skip)
    return  this.http.get<any>(`${this.apiUrl3+'&top='}${this.top}&skip=${this.skip}`).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }
  delete(id:any){
    return this.http.put(`${this.apiUrldelete}?id=${id}`,id).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }

}
