import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponentComponent } from './home-page-component/home-page-component.component';
import { AdvertismentDetailsComponent } from './advertisment-details/advertisment-details.component';
import { AdvertismentUserComponent } from './advertisment-user/advertisment-user.component';
import { FavoriteComponent } from './favorite/favorite.component';
import { PackageComponent } from 'src/admin/package/package.component';
import { PackagesComponent } from './packages/packages.component';


const routes: Routes = [

  //{path:"profile",component:UserprofileComponent}
 {path:"myprofile",loadChildren:()=>import("../profile/profile.module").then(m=>m.ProfileModule)},
  {path:'Home',component:HomePageComponentComponent},
  {path:'package',component:PackagesComponent},
  {path:'Details/:id',component:AdvertismentDetailsComponent},
  {path:'Favorite',component:FavoriteComponent},
  {path:'AdvertismetUser/:id',component:AdvertismentUserComponent},
   {path:'',component:HomePageComponentComponent},

  //{path:'',component:LandingComponent},
  {path:'authintication',loadChildren:()=>import("../authintication/authintication.module").then(m=>m.AuthinticationModule)},
  {path:'filteration',loadChildren:()=>import("../filteration/filteration.module").then(m=>m.FilterationModule)},
  {path:'admin',loadChildren:()=>import("../admin/admin.module").then(m=>m.AdminModule)},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
