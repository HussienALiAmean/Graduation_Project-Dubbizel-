import { Component } from '@angular/core';
import { AdvertismentServiceService } from '../Services/advertisment-service.service';
import { IAdvertisment } from '../Interfaces/IAdvertisment';

@Component({
  selector: 'app-my-ads',
  templateUrl: './my-ads.component.html',
  styleUrls: ['./my-ads.component.scss']
})
export class MyAdsComponent {

  LodedAdvertisments:IAdvertisment[]=[];
  FiltredAdvertisments:IAdvertisment[]=[];

  applicationUserId:any=localStorage.getItem('ApplicationUserId')
  AllAdsCounter:number=0;
  ActivedsCounter:number=0;
  InActivedsCounter:number=0;
  PendingdsCounter:number=0;
  ModerateddsCounter:number=0;

  constructor(private advertismentUserService :AdvertismentServiceService){}
  
  ngOnInit(): void {

      this.advertismentUserService.getMyAdvertisments(this.applicationUserId).subscribe({
        next:(data:any)=>{
          this.LodedAdvertisments=data.data;
          this.FiltredAdvertisments=this.LodedAdvertisments;
          this.counterAds();
        },
        error: err => {
          console.log(err);
        }
       })
  }

  counterAds()
  {
    this.LodedAdvertisments.forEach((ad:IAdvertisment)=>{
      this.AllAdsCounter++;
      if(ad.adStatus=="Active")
          this.ActivedsCounter++;
      else if (ad.adStatus=="Not Active")
          this.InActivedsCounter++;
      else if (ad.adStatus=="Pending")
          this.PendingdsCounter++;
      else 
          this.ModerateddsCounter++;
    })
  }

  AllAds()
  {
    this.FiltredAdvertisments=this.LodedAdvertisments;
  }

  ActiveAds()
  {
     this.FiltredAdvertisments = this.LodedAdvertisments.filter((ad:IAdvertisment) =>{
      return ad.adStatus=="Active";
  });

  }

  InactiveAds()
  {
    this.FiltredAdvertisments = this.LodedAdvertisments.filter((ad:IAdvertisment) =>{
      return ad.adStatus=="Not Active";
  });
  }

  PendingAds()
  {
    this.FiltredAdvertisments = this.LodedAdvertisments.filter((ad:IAdvertisment) =>{
      return ad.adStatus=="Pending";
  });
  }

  ModeratedAds()
  {
    this.FiltredAdvertisments = this.LodedAdvertisments.filter((ad:IAdvertisment) =>{
      return ad.adStatus=="Moderated";
  });
  }

}
