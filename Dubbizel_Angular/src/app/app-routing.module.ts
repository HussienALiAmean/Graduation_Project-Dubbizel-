import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  //{path:'',component:LandingComponent},
  {path:'authintication',loadChildren:()=>import("../authintication/authintication.module").then(m=>m.AuthinticationModule)},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
