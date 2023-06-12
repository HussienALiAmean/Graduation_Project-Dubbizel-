import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FilterationRoutingModule } from './filteration-routing.module';
import { FilterSideComponent } from './FilterSide/filter-side/filter-side.component';
import { AddvertisSideComponent } from './AddvertisSide/addvertis-side/addvertis-side.component';



@NgModule({
  declarations: [
    FilterSideComponent,
    AddvertisSideComponent
    
  ],
  imports: [
    CommonModule,
    FilterationRoutingModule,
  
  ]
  ,exports:[FilterSideComponent
  ,AddvertisSideComponent
  
]
})
export class FilterationModule { }
