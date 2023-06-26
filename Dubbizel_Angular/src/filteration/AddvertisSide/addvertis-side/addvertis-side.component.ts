import { Component } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { IFavourite } from 'src/app/Interface/IFavorite';
import { IAdvertisment } from 'src/app/Interfaces/IAdvertisment';
import { AdvertismentServiceService } from 'src/app/Services/advertisment-service.service';
import { FavoriteService } from 'src/app/Services/favorite.service';
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
Advertisments:IAdvertisment[]=[];
Loaded_dedaddvertisment:IAdvertisment[]=[];
filterationKeyArray:String[]=[];
cat_locationFilterationArry :String[]=[];
userId:any=localStorage.getItem('ApplicationUserId');
Favorite:IFavourite=new IFavourite("",0);
errorMessage:string=""


constructor(private activatRoute:ActivatedRoute,private advertismentService:AdvertismentServiceService,private router:Router,private favoriteService:FavoriteService)
{
  activatRoute.paramMap.subscribe((params:ParamMap)=>{
    console.log(params.get('id'))
    if(params.get('type')=='category')
    {
      console.log(true)
      this.advertismentService.getAdsByCategoryID(params.get('id'),this.userId).subscribe({
        next: (data:any) => {
          this.Loaded_dedaddvertisment=data.data;
          this.Advertisments=this.Loaded_dedaddvertisment;

        console.log(data);
        },
        error: err => {
          console.log(err);
        }
      }); 
    }
    else
    {
      this.advertismentService.getAdsBySubCategoryID(params.get('id'),this.userId).subscribe({
        next: (data:any) => {
          this.Loaded_dedaddvertisment=data.data;
          this.Advertisments=this.Loaded_dedaddvertisment;
        console.log(data);
        },
        error: err => {
          console.log(err);
        }
      }); 
      console.log(false)
    }
  })
}

async AddToFavorite(ads:any){
  var heart=document.getElementById("heart"+ads.id+this.userId);
  console.log(heart?.style.color);
  if(heart?.style.color=="rgb(255, 255, 255)"){
  //console.log("hi")
  this.Favorite.advertismentID=ads.id;
  this.Favorite.applicationUserId=this.userId;
  await this.favoriteService.AddFavorite(this.Favorite).subscribe({
  next:data=>console.log(data),
  error:error=>this.errorMessage=error
})
heart.style.color="rgb(224, 0, 0)";
}
else{
console.log("hi")
 this.favoriteService.DeleteFavorite(ads.id,this.userId).subscribe({
  next:data=>console.log(data),
  error:error=>this.errorMessage=error
 })
 heart!.style.color="rgb(255, 255, 255)";
}
}

AdvertismentDetails(a:any){
  this.router.navigate(["/Details",a.id]);
} 

BeginChat(applicationUserId:string)
{
  console.log(applicationUserId)
}

handleDataChange(newdata:String)
{  console.log(newdata);
  if (this.filterationKeyArray.includes(newdata))
  {
    console.log(this.filterationKeyArray=this.filterationKeyArray.filter(e=>e!=newdata));
  }
  else
  {
    this.filterationKeyArray.push(newdata)
    console.log(this.filterationKeyArray);
  }
  this.Advertisments=[];
  this.Advertisments=this.filterByCat_locatiomFilterationArray
  (this.makefilterationByCheckbox(this.filterationKeyArray,
    this.Loaded_dedaddvertisment),this.cat_locationFilterationArry);
}
handelChanOfCategory(newdata:String)
{
  this.cat_locationFilterationArry[0]=newdata;
  this.Advertisments=[];
  this.Advertisments=this.filterByCat_locatiomFilterationArray(this.makefilterationByCheckbox(this.filterationKeyArray,this.Loaded_dedaddvertisment),this.cat_locationFilterationArry);
  // this.Advertisments=this.filterByCat_locatiomFilterationArray(this.Loaded_dedaddvertisment,this.cat_locationFilterationArry);
}
handelChanOflocation(newdata:String)
{   console.log(newdata);
    this.cat_locationFilterationArry[1]=newdata;
    this.Advertisments=[];
    this.Advertisments=this.filterByCat_locatiomFilterationArray(this.makefilterationByCheckbox(this.filterationKeyArray,this.Loaded_dedaddvertisment),this.cat_locationFilterationArry);
    //this.Advertisments=this.filterByCat_locatiomFilterationArray(this.Loaded_dedaddvertisment,this.cat_locationFilterationArry);
}
filterByCat_locatiomFilterationArray(advertisment:IAdvertisment[],cat_LocFilterArry:String[]):IAdvertisment[]
{
  if(cat_LocFilterArry.length==0)
  { console.log("1#");
    return advertisment;
  }
  else if (cat_LocFilterArry.length==1 && cat_LocFilterArry[0]!=null)
  { 
    console.log("2#");
    if(Number(cat_LocFilterArry[0])==0)
    return  advertisment;
    else
    return  advertisment.filter(item =>item.subCategoryID==Number(cat_LocFilterArry[0]) );

  }
  else if (cat_LocFilterArry.length==1 && cat_LocFilterArry[1]!=null)
  {
    console.log("3#");
    return advertisment.filter(item =>item.location==cat_LocFilterArry[1] );
  }
  else if (cat_LocFilterArry.length==2 && cat_LocFilterArry[0]=="0" && cat_LocFilterArry[1]!=null && cat_LocFilterArry[1]!="all")
  {
    console.log("3#2");
    return advertisment.filter(item =>item.location==cat_LocFilterArry[1] );
  }
  else if (cat_LocFilterArry.length==2 && cat_LocFilterArry[1]=="all" && cat_LocFilterArry[0]!=null && cat_LocFilterArry[0]!="0")
  {
    console.log("3#3");
    return advertisment.filter(item =>item.subCategoryID==Number(cat_LocFilterArry[0]));
  }
  else if(cat_LocFilterArry.length==2 &&cat_LocFilterArry[1]!="all"&& cat_LocFilterArry[0]!="0")
  {
    console.log("4#");
    return advertisment.filter(item =>item.subCategoryID==Number(cat_LocFilterArry[0]) &&item.location==cat_LocFilterArry[1] );
  }
  return advertisment;
}
makefilterationByCheckbox( filterationKeyArray: String[],advertismentArray: IAdvertisment[]): IAdvertisment[]
{
  if(filterationKeyArray.length>0)
  {
    return  advertismentArray.filter(item => {
      return item.advertisment_FiltrationValuesList.some(filterValue => {
        console.log(filterationKeyArray.includes(filterValue));
        return filterationKeyArray.includes(filterValue);
    });
   });
  }
  else 
  {
    return advertismentArray;
  } 
}




// makefilterationByCheckbox(filterationKeyArray:String[] ,advertismentArray:IAdvertisment[] ):IAdvertisment[]
// {

//   if(this.filterationKeyArray.length > 0)
//   {
//     let addvertisment:IAdvertisment[] =[];
//     advertismentArray.forEach(item =>
//       {
//         let for2Condition=false;
//         item.advertisment_FiltrationValuesList.forEach(Advertes_filterationItem=>
//           {
//               let forCondition=false;
//               filterationKeyArray.forEach(filterationcondiotn =>
//                 {
//                   console.log(filterationcondiotn);
//                   if(Advertes_filterationItem==filterationcondiotn)     
//                   {
//                     forCondition=true;
//                     console.log(filterationcondiotn);
//                   }
//                 });
//               if (forCondition){ for2Condition=true;}
          
//           });
//           if (for2Condition)
//           {
//             addvertisment.push(item);
//           }

//       });
//       return addvertisment;
//   }
//   else
//   {
//     return advertismentArray;
//   }
// }

}
