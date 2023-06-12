import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from 'src/home-page/home/home.component';
import { HomePageComponentComponent } from './home-page-component/home-page-component.component';

const routes: Routes = [
  {path:'Home',component:HomePageComponentComponent},
  {path:'Home/:id',component:HomePageComponentComponent},
  {path:'home-page',loadChildren:()=>import("../home-page/home-page.module").then(m=>m.HomePageModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
