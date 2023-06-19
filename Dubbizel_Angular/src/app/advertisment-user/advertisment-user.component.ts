import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdvertismentServiceService } from '../Services/advertisment-service.service';

@Component({
  selector: 'app-advertisment-user',
  templateUrl: './advertisment-user.component.html',
  styleUrls: ['./advertisment-user.component.scss']
})
export class AdvertismentUserComponent implements OnInit {
  userId:any;
  advertismentUser:any;
  constructor(private activeRoute:ActivatedRoute, private advertismentUserService :AdvertismentServiceService,private router:Router){}
  ngOnInit(): void {
    this.userId=this.activeRoute.snapshot.paramMap.get("id");

      this.advertismentUserService.getAdvertismentUser(this.userId).subscribe({
        next:data=>{
          this.advertismentUser=data
        },
    
       })
  }

  AdvertismentUserDetails(a:any){
   this.router.navigate(["/Details",a.id])
  }
}
