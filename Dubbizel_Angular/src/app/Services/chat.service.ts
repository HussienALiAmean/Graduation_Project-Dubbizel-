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
  private apiUrlUsers = 'http://localhost:7189/api/User?id='
  private apiUrl = "http://localhost:7189/api/Chat/GetChatUsers?id=" //'http://localhost:7189/api/User'
  private apiUrl3 = "http://localhost:7189/api/Chat/GetMessages?" //'http://localhost:7189/api/User'
  private apiUrl2 = "http://localhost:7189/api/Chat/AddMessage" //'http://localhost:7189/api/User'
  private apiUrldelete = "http://localhost:7189/api/Chat" //'http://localhost:7189/api/User'
  constructor(private http:HttpClient) { }

  GetUsers(id:any,loginId:any):Observable<IUser[]>{
    console.log('reach')
    console.log(`${this.apiUrl}${id}&loginId=${loginId}`)
    
    return this.http.get<IUser[]>(`${this.apiUrl}${id}&loginId=${loginId}`).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }

  AddMessage(data:any){
    return this.http.post(this.apiUrl2,data).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }
  getChat(sender:any,receiver:any){
    console.log(this.top)
    console.log(this.skip)
    return  this.http.get<any>(`${this.apiUrl3+'sender='}${sender}&reciver=${receiver}&top=${this.top}&skip=${this.skip}`).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }
  delete(id:any){
    return this.http.put(`${this.apiUrldelete}?id=${id}`,id).pipe(catchError((err: any) => {
      return throwError(() => err.message || "server error");
      }));
  }

}
