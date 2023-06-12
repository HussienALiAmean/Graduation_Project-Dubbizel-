import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from 'src/home-page/home/home.component';
import { HomePageComponentComponent } from './home-page-component/home-page-component.component';

const routes: Routes = [
  
  {path:'Home/:id',component:HomePageComponentComponent},
  {path:'home-page',loadChildren:()=>import("../home-page/home-page.module").then(m=>m.HomePageModule)},

  //{path:'',component:LandingComponent},
  {path:'authintication',loadChildren:()=>import("../authintication/authintication.module").then(m=>m.AuthinticationModule)},
  {path:'filteration',loadChildren:()=>import("../filteration/filteration.module").then(m=>m.FilterationModule)},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
