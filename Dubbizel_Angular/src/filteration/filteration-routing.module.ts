import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowAddvertisComponent } from './show-addvertis/show-addvertis.component';

const routes: Routes = [
  {path:'ShowAd',component:ShowAddvertisComponent},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FilterationRoutingModule { }
