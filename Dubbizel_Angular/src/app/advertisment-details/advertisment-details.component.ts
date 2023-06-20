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

export class AdvertismentDetailsComponent implements OnInit{
  advertismentId: any
  reviewList: IReview[] = []
  private hubConnectionBuilder!: HubConnection
  appUserId: any;
  username :any
  editIndex: any;
  reviewId : any;
  advertismentDetail:any={};
  FirstChar:string="";
  reviewAdded!: IReview
  constructor( private fb: FormBuilder, private reviewService: ReviewserviceService, private advertismentService:AdvertismentServiceService, private activeRoute:ActivatedRoute,private router:Router){}
  
  ReviewForm: any = this.fb.group({
    id:[0],
    text: [''],
    rate: [''],
    userName: [localStorage.getItem("UserName")],
    autherId: [localStorage.getItem("ApplicationUserId")],
    advertismentID: [this.activeRoute.snapshot.paramMap.get("id")],
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
 
    this.appUserId = localStorage.getItem("ApplicationUserId");
    this.advertismentId = this.activeRoute.snapshot.paramMap.get("id");
    this.username=localStorage.getItem("UserName");

    // this.reviewAdded = {
    //   text: "", advertismentID: this.advertismentId, rate: 0,
    //    autherId: this.appUserId, userName: this.username, id: this.reviewId
    // }

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
      
      console.log(this.ReviewForm.value)

  //     this.hubConnectionBuilder.invoke('NewReview', this.ReviewForm.value);
  //     await this.hubConnectionBuilder.on('NewReviewNotify', (rev) => {
  //     console.log(rev)
  //     this.advertismentDetail.reviewsList.push(rev)
  // });

      this.reviewService.AddReview(this.ReviewForm.value).subscribe({
        next: (data:any) => {
          console.log(data);
        //  this.ReviewForm.get('id').value=data.id;
        this.advertismentDetail.reviewsList.push(data)
        },
        error: error => console.log(error),
      });
    }



     editreview(id: any, i: any,TextInput:any,RateInput:any,SaveBtn:any) {

      TextInput.disabled=RateInput.readonly=false;
      SaveBtn.hidden=false;

      this.editIndex=i;
      this.reviewId=id;
    
    }

    async SaveEdit(TextInput:any,RateInput:any,SaveBtn:any) {

      this.ReviewForm.patchValue({
        id:this.reviewId,
        text: TextInput.value,
        rate: RateInput.rate
      })

         // this.hubConnectionBuilder.invoke('EditReview', this.ReviewForm.value);
      // await this.hubConnectionBuilder.on('EditReviewNotify', (rev) => {
      //   console.log(this.reviewList.indexOf(rev));
      // });


      this.reviewService.EditReview(this.ReviewForm.value).subscribe({
        next: data => {
          console.log(data);
          this.advertismentDetail.reviewsList[this.editIndex]=data;
        },
        error: error => console.log(error),
      });

      TextInput.disabled=RateInput.readonly=true;
      SaveBtn.hidden=true

    }



    async delreview(rl: any, i: any) {
     
      this.reviewId=rl.id;

      // this.ReviewForm.patchValue({
      //   id:rl.id,
      //   text: rl.text,
      //   rate: rl.rate
      // })

      // this.hubConnectionBuilder.invoke('RemoveReview', this.ReviewForm.value);
      // this.hubConnectionBuilder.on('RemoveReviewNotify', (rev) => {
      //   this.reviewList.splice(rev, 1)
      // });
  
  
      this.reviewService.DeleteReview(this.reviewId).subscribe({
        next: data => {
          console.log(data);
          this.advertismentDetail.reviewsList.splice(i,1);
        },
        error: error => console.log(error),
      });

    }
  
    




}
