import { Component, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ICategory } from 'src/app/Interfaces/ICategory';
import { ISubCategoryFilter } from 'src/app/Interfaces/ISubCategoryFilter';
import { FiltrationServiceService } from 'src/app/Services/filtration-service.service';
import { CategoryServiceService } from '../../../app/Services/category-service.service';


@Component({
  selector: 'app-filter-side',
  templateUrl: './filter-side.component.html',
  styleUrls: ['./filter-side.component.scss']
})
export class FilterSideComponent {
  categorySelectedValue: String="";
  LocationSelectedValue: String="";
  SubCategoryFilters:ISubCategoryFilter[]=[];
  @Output() dataChanged = new EventEmitter<String>();
  @Output() filterByCategory = new EventEmitter<String>();
  @Output() filterBylocation = new EventEmitter<String>();
  locationList:String[]=["Cairo","Asuuit","Sohag","Alex"];
  categoryList:ICategory[]=[];
  makefilter(newData: string)
  {
    this.dataChanged.emit(newData);
  }
  changeCategoryFiltertion() 
  {
    this.filterByCategory.emit(this.categorySelectedValue);
  }
  changelocationFiltertion() 
  {
    console.log("changed"+this.LocationSelectedValue);
    this.filterBylocation.emit(this.LocationSelectedValue);
  }


  constructor(private activatRoute:ActivatedRoute,private filterSrvice:FiltrationServiceService,private _CategoryServiceService:CategoryServiceService)
  {
      activatRoute.paramMap.subscribe((params:ParamMap)=>{
          if(params.get('type')!='category')
          {
            console.log(true)
            this.filterSrvice.getSubCategoryFilters(params.get('id')).subscribe({
              next: (data:any) => {
                this.SubCategoryFilters=data.data;
                console.log("done 2");
                console.log(this.SubCategoryFilters);
                this._CategoryServiceService.getAllofsubCategories(params.get('id')).subscribe({
                  next: (data:any) => {
                    console.log("data arrived correctly"+data);
                    this.categoryList=data.data;
                  },
                  error: (err:any) =>{
                    console.log(err);
                  }
                })
              },
              error: err => {
                console.log(err);
              }
            }); 
          }
          if(params.get('type')=='category')
          {
                this._CategoryServiceService.getAllofsubCategories(params.get('id')).subscribe({
                  next: (data:any) => {
                    console.log("data arrived correctly"+data);
                    this.categoryList=data.data;
                  },
                  error: (err:any) =>{
                    console.log(err);
                  }
                })
          }
      });
     
  }
}
