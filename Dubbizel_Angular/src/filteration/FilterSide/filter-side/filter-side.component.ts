import { Component, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { ISubCategoryFilter } from 'src/app/Interfaces/ISubCategoryFilter';
import { FiltrationServiceService } from 'src/app/Services/filtration-service.service';


@Component({
  selector: 'app-filter-side',
  templateUrl: './filter-side.component.html',
  styleUrls: ['./filter-side.component.scss']
})
export class FilterSideComponent {
  SubCategoryFilters:ISubCategoryFilter[]=[];
  @Output() dataChanged = new EventEmitter<string>();

  makefilter(newData: string) {
    this.dataChanged.emit(newData);
  }
  constructor(private activatRoute:ActivatedRoute,private filterSrvice:FiltrationServiceService)
  {
      activatRoute.paramMap.subscribe((params:ParamMap)=>{
      console.log(params.get('id'))
      if(params.get('type')!='category')
      {
        console.log(true)
        this.filterSrvice.getSubCategoryFilters(params.get('id')).subscribe({
          next: (data:any) => {
            this.SubCategoryFilters=data.data;
          console.log(data);
          },
          error: err => {
            console.log(err);
          }
        }); 
      }
    });
  }
}
