import { Component,OnInit } from '@angular/core';
import { AdvertismentServiceService } from '../Services/advertisment-service.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-advertisment-details',
  templateUrl: './advertisment-details.component.html',
  styleUrls: ['./advertisment-details.component.scss']
})
export class AdvertismentDetailsComponent implements OnInit{
  advertismentId:any;
  advertismentDetail:any={};
  FirstChar:string="";
  constructor(private advertismentService:AdvertismentServiceService, private activeRoute:ActivatedRoute,private router:Router){}
    ngOnInit(): void {
      this.advertismentId=this.activeRoute.snapshot.paramMap.get("id");

      this.advertismentService.getDetails(this.advertismentId).subscribe({
        next:data=>{
          this.advertismentDetail=data
        },
    
       })

       this.advertismentService.getDetails(this.advertismentId).subscribe({
        next:data=>{
          this.FirstChar=data.applicationUserName.charAt(0)
        },
    
       })
  
    }

  AdvertismentUser(advertismentDetail:any){
     this.router.navigate(["/AdvertismetUser",advertismentDetail.applicationUserId])
    } 
}
