import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponentComponent } from './home-page-component/home-page-component.component';
import { ChatComponent } from './chatpage/chat/chat.component';
import { AdvertismentDetailsComponent } from './advertisment-details/advertisment-details.component';
import { AdvertismentUserComponent } from './advertisment-user/advertisment-user.component';
import { FavoriteComponent } from './favorite/favorite.component';

import { PackageComponent } from 'src/admin/package/package.component';
import { PackagesComponent } from './packages/packages.component';
import { MyAdsComponent } from './my-ads/my-ads.component';
import { CategoriesBeforePackagesComponent } from './categories-before-packages/categories-before-packages.component';









const routes: Routes = [

  {path:'',component:HomePageComponentComponent},

  //{path:"profile",component:UserprofileComponent}
 {path:"chat/:adId/:UserID",component:ChatComponent},

  {path:'Home/:id',component:HomePageComponentComponent},
  // {path:'home-page',loadChildren:()=>import("../home-page/home-page.module").then(m=>m.HomePageModule)},

 {path:"myprofile",loadChildren:()=>import("../profile/profile.module").then(m=>m.ProfileModule)},
  {path:'Home',component:HomePageComponentComponent},
  {path:'categories',component:CategoriesBeforePackagesComponent},
  {path:'package/:id',component:PackagesComponent},
  {path:'Details/:id',component:AdvertismentDetailsComponent},
  {path:'Favorite',component:FavoriteComponent},
  {path:'MyAds',component:MyAdsComponent},
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
