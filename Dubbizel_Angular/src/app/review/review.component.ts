import { Component } from '@angular/core';
import { IReview } from '../Interfaces/Review';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReviewserviceService } from '../Services/reviewservice.service';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import {Injectable} from "@angular/core";
@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})

@Injectable({
  providedIn: 'root'
})
export class ReviewComponent {
  Adid:number=3
  reviewList:IReview[]=[]
  reviewAdded!:IReview
  private hubConnectionBuilder!:HubConnection
  rateReview=0;
  autherId="99ba0e2f-a547-44ae-b85e-04b36dbeff4c" ;
  appUserId=localStorage.getItem("ApplicationUserId");
  reviewId=14;
  username="Aya"
  editRev:any=[];
  editIndex:any

  constructor(private http:HttpClient,private fb:FormBuilder,private formbuilder:FormBuilder,private reviewService:ReviewserviceService){}

  ReviewForm:any=this.fb.group({
   text:[''],
   rate:[''],
   userName:[''],
   autherId: [''],
  advertismentID: [''],
  })

get text()
{
  return this.ReviewForm.get('text');
}
get rate()
{
  return this.ReviewForm.get('rate');
}
    
  
async ngOnInit():Promise<void>
{
  //this.reviewId = localStorage.getItem("reviewId");
  this.reviewService.GetAllReviews().subscribe({
    next:(data:any)=> {
      this.reviewList = data
      console.log(this.reviewList)

    },
    error: error => console.log(error),
  })
  //id details
  // this.activatedroute.paramMap.subscribe(paramMap => {
  //   this.Adid = Number(paramMap.get('Adid'));
  //   console.log(typeof (this.Adid));
  //   this.http.get(`http://localhost:7189/api/Advertisment/` + this.Adid).subscribe({
  //     next: data => {
  //       //console.log(data);
  //       this.Resturant = data
  //     },

  //     error: error => console.log(error)

  //   });
  //   console.log(typeof (this.Adid));

  // })

  this.reviewAdded = { text: "", advertismentID: this.Adid, rate: this.rateReview 
  ,autherId:this.autherId,userName:this.username,ID:this.reviewId}

  this.hubConnectionBuilder = new signalR.HubConnectionBuilder().withUrl('http://localhost:7189/Review',
    {
      
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
    }).configureLogging(signalR.LogLevel.Debug).build();

  await setTimeout(() => {
    this.hubConnectionBuilder.start().then(() => {
      console.log("connection started");
    }).catch(err => console.log(err));
  }, 2000);


}
async sendReview() 
{
  this.hubConnectionBuilder.invoke('NewReview',this.reviewAdded);
  await this.hubConnectionBuilder.on('NewReviewNotify',(rev) => {
    console.log(rev)
    this.reviewList.unshift(rev)
  
  });
      
   this.reviewService.AddReview(this.reviewAdded,this.autherId).subscribe({
    next:data=>{
      console.log(data)

    },
    error:error=>console.log(error),
  });
}
async delreview(rl:any,i:any)
{
  console.log(rl)
  this.reviewAdded.text=rl.text;
  this.reviewAdded.rate=rl.rate; 
  this.reviewAdded.ID=rl.ID
  console.log(this.reviewAdded.ID)
  this.hubConnectionBuilder.invoke('RemoveReview',this.reviewAdded);
 
  await this.hubConnectionBuilder.on('RemoveReviewNotify',(rev) => {
  
    console.log(this.reviewList.splice(rev,1));
    this.reviewList.splice(rev,1)
  });

  this.reviewService.DeleteReview(this.reviewId).subscribe({
    next:data=>{
      console.log(data);
    },
    error:error=>console.log(error),
   
  });
}

async editreview(rl:any,i:any)
{
  this.hubConnectionBuilder.invoke('RemoveReview',this.reviewId);
  await this.hubConnectionBuilder.on('RemoveReviewNotify',(rev) => {
    console.log(this.reviewList.indexOf(rev));
    //this.reviewList.indexOf(rev)
  });
      this.ReviewForm.patchValue({

        text:rl.text,
        rate:rl.rate,
        userName:rl.userName,
        autherId: rl.autherId,
        advertismentID: rl.advertismentID,
      })
      console.log(this.ReviewForm.value)

  // this.reviewService.EditReview(this.reviewId,this.ReviewForm.value).subscribe({
  //   next:data=>{
  //     console.log(this.ReviewForm.value)
  //     this.editRev=data;
  //     console.log(data);
  //   },
  //   error:error=>console.log(error),
  // });

}
}
