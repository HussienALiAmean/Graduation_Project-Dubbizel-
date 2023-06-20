import { Component,OnInit } from '@angular/core';
import { AdvertismentServiceService } from '../Services/advertisment-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReviewserviceService } from '../Services/reviewservice.service';
import { IReview } from '../Interfaces/Review';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Injectable } from "@angular/core";

@Component({
  selector: 'app-advertisment-details',
  templateUrl: './advertisment-details.component.html',
  styleUrls: ['./advertisment-details.component.scss']
})
export class AdvertismentDetailsComponent{
  advertismentId: any
  reviewList: IReview[] = []
  private hubConnectionBuilder!: HubConnection
  appUserId: any;
  reviewId = 13;
  rateReview = 0
  username = "Aya"
  editRev: any = [];
  editIndex: any
  isEnabledText = true;
  isEnableRate = true
  isEnabledSaveBtn = true
  edited = false
  advertismentDetail:any={};
  FirstChar:string="";
  authId: any
  reviewAdded!: IReview
  constructor( private fb: FormBuilder, private reviewService: ReviewserviceService, private advertismentService:AdvertismentServiceService, private activeRoute:ActivatedRoute,private router:Router){}
  ReviewForm: any = this.fb.group({
    text: [''],
    rate: [''],
    userName: [''],
    autherId: [''],
    advertismentID: [''],
  })

  get text() {
    return this.ReviewForm.get('text');
  }
  get rate() {
    return this.ReviewForm.get('rate');
  }
  get userName() {
    return this.ReviewForm.get('userName');
  }
  get autherId() {
    return this.ReviewForm.get('autherId');
  }
  get advertismentID() {
    return this.ReviewForm.get('advertismentID');
  }

  async ngOnInit(): Promise<void> {
 
    this.appUserId = localStorage.getItem("ApplicationUserId")
    this.advertismentId = this.activeRoute.snapshot.paramMap.get("id");
    this.reviewAdded = {
      text: "", advertismentID: this.advertismentId, rate: this.rateReview
      , autherId: this.authId, userName: this.username, ID: this.reviewId
    }

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

       this.advertismentService.getDetails(this.advertismentId).subscribe({
        next:data=>{
          console.log(data)
          this.advertismentDetail=data
        },
        error: err => {
          console.log(err);
        }
    
       })

       this.advertismentService.getDetails(this.advertismentId).subscribe({
        next:data=>{
          console.log(data)
          this.FirstChar=data.applicationUserName.charAt(0)
        },
        error: err => {
          console.log(err);
        }
    
       })

    }


    
  AdvertismentUser(advertismentDetail:any){
     this.router.navigate(["/AdvertismetUser",advertismentDetail.applicationUserId])
    } 





    async sendReview() {
      this.hubConnectionBuilder.invoke('NewReview', this.reviewAdded);
      this.hubConnectionBuilder.on('NewReviewNotify', (rev) => {
        console.log(rev)
        this.reviewList.unshift(rev)
  
      });
  
      this.reviewService.AddReview(this.reviewAdded, this.appUserId).subscribe({
        next: data => {
          console.log(data)
  
        },
        error: error => console.log(error),
      });
    }
    async delreview(rl: any, i: any) {
      console.log(rl)
      this.reviewAdded.text = rl.text;
      this.reviewAdded.rate = rl.rate;
      this.reviewAdded.ID = rl.ID
      console.log(this.reviewAdded.ID)
      this.hubConnectionBuilder.invoke('RemoveReview', this.reviewAdded);
      this.hubConnectionBuilder.on('RemoveReviewNotify', (rev) => {
        this.reviewList.splice(rev, 1)
      });
  
  
      this.reviewService.DeleteReview(this.reviewId).subscribe({
        next: data => {
          console.log(data);
        },
        error: error => console.log(error),
  
      });
    }
  
    async editreview(rl: any, i: any) {
      this.isEnabledText = false;
      this.isEnableRate = false;
      this.isEnabledSaveBtn = false;
      this.edited = true;
  
      this.hubConnectionBuilder.invoke('EditReview', this.reviewId);
      await this.hubConnectionBuilder.on('EditReviewNotify', (rev) => {
        console.log(this.reviewList.indexOf(rev));
      });
      this.ReviewForm.patchValue({
  
        text: rl.text,
        rate: rl.rate,
        userName: rl.userName,
        autherId: rl.autherId,
        advertismentID: rl.advertismentID,
      })
      console.log(this.ReviewForm.value)
    }
    SaveEdit() {
      this.reviewService.EditReview(this.reviewId, this.ReviewForm.value).subscribe({
        next: data => {
          console.log(this.ReviewForm.value)
          this.editRev = data;
          console.log(data);
        },
        error: error => console.log(error),
      });
    }




}
