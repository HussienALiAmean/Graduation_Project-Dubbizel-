import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { IAdvertisment } from 'src/app/Interfaces/IAdvertisment';
import { AdvertismentServiceService } from 'src/app/Services/advertisment-service.service';
@Component({
  selector: 'app-addvertis-side',
  templateUrl: './addvertis-side.component.html',
  styleUrls: ['./addvertis-side.component.scss']
})
export class AddvertisSideComponent {
 items:any[]=[{id:1,name:"Fiat",description:"goood 4 douresgoood 4 douresgoood 4 douresgoood 4 doures",price:1500,rate:5,imagesrc:"../../../assets/0.jpg" ,havechat:true}
 ,{id:1,name:"Marcedes",description:"goood 4 douresgoood 4 douresgoood 4 doures",price:300058,rate:2,imagesrc:"../../../assets/images.jpg"}
 ,{id:1,likedbyme:true,name:"Cmonda",description:"laovasdafas  dsaff fsfsadadous  dsafsafsafsff fsfsadadou",price:4863321,rate:6,imagesrc:"../../../assets/images(3).jpg",havechat:true}
 ,{id:1,name:"Change",description:"goood kondato makoatin as 4 doures",price:120000,rate:9,imagesrc:"../../../assets/images.jpg"}
 ,{id:1,likedbyme:true,name:"Donato",description:"goood 4 2e2ewadzc wesadd3re32rd 2edw",price:700054,rate:9,imagesrc:"../../../assets/0.jpg",havechat:true}
 ,{id:1,name:"Cmonda",description: "laovasdafas  dsafsafsafsff lore fsfsadadoures",price:4863321,rate:6,imagesrc:"../../../assets/images(3).jpg",havechat:true}
 ,{id:1,name:"Marcedes",description:"goood 4 douresgoood 4 douresgoood doures",price:300058,rate:2,imagesrc:"../../../assets/images.jpg",havechat:true}
 ,{id:1,likedbyme:true, name:"Change",description:"goood kondato  makoatin 4 makoatin as 4 makoatin as 4 doures",price:120000,rate:9,imagesrc:"../../../assets/images.jpg"}
];
Advertisments:IAdvertisment[]=[]

constructor(private activatRoute:ActivatedRoute,private advertismentService:AdvertismentServiceService)
{
  this.advertismentService.getAds().subscribe({
    next: (data:any) => {
      this.Advertisments=data.data;
    console.log(data);
    },
    error: err => {
      console.log(err);
    }
  }); 
  activatRoute.paramMap.subscribe((params:ParamMap)=>{
    console.log(params.get('id'))
    if(params.get('type')=='category')
    {
      console.log(true)
    }
    else
    {
      console.log(false)
    }
  })
  
}
}
