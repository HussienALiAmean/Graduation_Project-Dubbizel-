import { Component ,OnInit} from '@angular/core';
import { HomeServiceService } from 'src/app/Services/home-service.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  categories:any=[]
  errorMessage:string=""
  constructor(private homeService:HomeServiceService){}
  ngOnInit(): void {
    this.homeService.getCategoriesWithSubAndAds().subscribe({
      next:data=>this.categories=data,
      error:error=>this.errorMessage=error
    })
  }

}
