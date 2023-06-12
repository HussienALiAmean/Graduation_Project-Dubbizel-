import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserprofileComponent } from './userprofile/userprofile.component';

const routes: Routes = [
  //../profile/profile.module
 //{path:"myprofile",loadChildren:()=>import("src/modules/profile/profile.module").then(m=>m.ProfileModule)}

{path:"profile",component:UserprofileComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

 }
