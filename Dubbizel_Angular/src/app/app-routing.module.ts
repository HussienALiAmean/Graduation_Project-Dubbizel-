import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserprofileComponent } from './userprofile/userprofile.component';
import { HomeComponent } from 'src/home-page/home/home.component';
import { HomePageComponentComponent } from './home-page-component/home-page-component.component';
import { ChatComponent } from './chatpage/chat/chat.component';




const routes: Routes = [
  //{path:"profile",component:UserprofileComponent}
 {path:"myprofile",loadChildren:()=>import("src/modules/profile/profile.module").then(m=>m.ProfileModule)},
 {path:"chat",component:ChatComponent},

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
export class AppRoutingModule {

 }
