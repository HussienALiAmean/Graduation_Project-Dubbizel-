import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from 'src/home-page/home/home.component';
import { HomePageComponentComponent } from './home-page-component/home-page-component.component';

import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CatNavBarComponent } from './cat-nav-bar/cat-nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';


export function tokenGetter() {
  return localStorage.getItem("jwt");
}
import { FilterationModule } from 'src/filteration/filteration.module';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponentComponent,

    NavBarComponent,
    CatNavBarComponent,
    FooterComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,

    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
       allowedDomains: ["localhost:7189"],
        disallowedRoutes: []
      }
  }),

    FilterationModule,
  ],
  providers: [MatIconRegistry],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private matIconRegistry: MatIconRegistry, private domSanitizer: DomSanitizer) {
    this.matIconRegistry.addSvgIcon(
      'icon_name',
      this.domSanitizer.bypassSecurityTrustResourceUrl('path/to/icon.svg')
    );
  }
 }
