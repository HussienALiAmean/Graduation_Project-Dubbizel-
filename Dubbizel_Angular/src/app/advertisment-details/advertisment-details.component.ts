import { Component,OnInit } from '@angular/core';
import { AdvertismentServiceService } from '../Services/advertisment-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IFavourite } from '../Interface/IFavorite';
import { FavoriteService } from '../Services/favorite.service';

@Component({
  selector: 'app-advertisment-details',
  templateUrl: './advertisment-details.component.html',
  styleUrls: ['./advertisment-details.component.scss']
})
export class AdvertismentDetailsComponent implements OnInit{
  advertismentId:any;
  advertismentDetail:any={};
  FirstChar:string="";
  userId:any
  Favorite:IFavourite=new IFavourite("",0);
  errorMessage:string=""
  id:any;
  isSaved:boolean=false
  constructor(private favoriteService:FavoriteService,private advertismentService:AdvertismentServiceService, private activeRoute:ActivatedRoute,private router:Router){}
    ngOnInit(): void {
      this.userId=localStorage.getItem("ApplicationUserId")

      this.advertismentId=this.activeRoute.snapshot.paramMap.get("id");

      this.advertismentService.getDetails(this.advertismentId,this.userId).subscribe({
        next:data=>{
          this.advertismentDetail=data
        },
    
       })

       this.advertismentService.getDetails(this.advertismentId,this.userId).subscribe({
        next:data=>{
          this.id=data.id
        },
    
       })
       this.advertismentService.getDetails(this.advertismentId,this.userId).subscribe({
        next:data=>{
          this.isSaved=data.isSaved
        },
    
       })

       this.advertismentService.getDetails(this.advertismentId,this.userId).subscribe({
        next:data=>{
          this.FirstChar=data.applicationUserName.charAt(0)
        },
    
       })
  
    }

  AdvertismentUser(advertismentDetail:any){
     this.router.navigate(["/AdvertismetUser",advertismentDetail.applicationUserId])
    } 


    async AddToFavorite(){
      var heart=document.getElementById("heart"+this.advertismentDetail.id+this.userId);
      console.log("heart",this.id,this.userId);
      if(heart?.style.color=="rgb(255, 255, 255)"){
      //console.log("hi")
      this.Favorite.advertismentID=this.id;
      this.Favorite.applicationUserId=this.userId;
      await this.favoriteService.AddFavorite(this.Favorite).subscribe({
      next:data=>console.log(data),
      error:error=>this.errorMessage=error
    })
    heart.style.color="rgb(224, 0, 0)";
  }
  else{
    console.log("hi")
     this.favoriteService.DeleteFavorite(this.id,this.userId).subscribe({
      next:data=>console.log(data),
      error:error=>this.errorMessage=error
     })
    heart!.style.color="rgb(255, 255, 255)";
  }
    }
}
