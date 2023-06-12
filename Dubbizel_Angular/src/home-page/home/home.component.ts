import { Component ,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { HomeServiceService } from 'src/app/Services/home-service.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  categories:any=[]
  errorMessage:string=""
  constructor(private homeService:HomeServiceService,private router:Router){}
  ngOnInit(): void {
    this.homeService.getCategoriesWithSubAndAds().subscribe({
      next:data=>this.categories=data,
      error:error=>this.errorMessage=error
    })
  }
  AdvertismentsByCategory(c:any){
    this.router.navigate(["/filteration/Advertisment/",c.id,'category'])
}
AdvertismentsBySubCategory(s:any){
  this.router.navigate(["/filteration/Advertisment/",s.id,'subcategory'])
}

}
