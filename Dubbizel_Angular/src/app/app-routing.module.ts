import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  //{path:'',component:LandingComponent},
  {path:'authintication',loadChildren:()=>import("../authintication/authintication.module").then(m=>m.AuthinticationModule)},
  {path:'filteration',loadChildren:()=>import("../filteration/filteration.module").then(m=>m.FilterationModule)},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
