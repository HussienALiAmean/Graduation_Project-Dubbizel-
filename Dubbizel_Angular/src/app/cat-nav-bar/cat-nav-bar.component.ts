import { Component, OnInit } from '@angular/core';
import { CategoryServiceService } from '../Services/category-service.service';
import { Router } from '@angular/router';
import { ICategory } from '../Interfaces/ICategory';

@Component({
  selector: 'app-cat-nav-bar',
  templateUrl: './cat-nav-bar.component.html',
  styleUrls: ['./cat-nav-bar.component.scss']
})

export class CatNavBarComponent implements OnInit {

  categories:ICategory[]=[]
  constructor(private categoryService:CategoryServiceService ,private router: Router) { }

  

  ngOnInit() {
  this.categoryService.getCategories().subscribe({
    next: (data:any) => {
    console.log(data);
    this.categories=data.data
   //this.router.navigate(["/resturant/profile"]);
    },
    error: err => {
      console.log(err);
    }
  }); 

  }

}
