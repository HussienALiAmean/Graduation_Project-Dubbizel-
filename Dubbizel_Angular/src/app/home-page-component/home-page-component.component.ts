import { Component,OnInit } from '@angular/core';
import { HomeServiceService } from '../Services/home-service.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-home-page-component',
  templateUrl: './home-page-component.component.html',
  styleUrls: ['./home-page-component.component.scss']
})
export class HomePageComponentComponent implements OnInit{
  categories:any=[]
  errorMessage:string=""
constructor(private homeService:HomeServiceService, private router:Router){}
  ngOnInit(): void {
    this.homeService.getCategoriesWithSubAndAds().subscribe({
      next:data=>this.categories=data,
      error:error=>this.errorMessage=error
    })
  }

  AdvertismentsByCategory(c:any){
      this.router.navigate(["/Home",c.id])
  }
  AdvertismentsBySubCategory(s:any){
    this.router.navigate(["/Home",s.id])
  }

}
