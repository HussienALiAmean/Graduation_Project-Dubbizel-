import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
 
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HomeComponent } from 'src/home-page/home/home.component';
import { HomePageComponentComponent } from './home-page-component/home-page-component.component';

import { NavBarComponent } from './nav-bar/nav-bar.component';
import { CatNavBarComponent } from './cat-nav-bar/cat-nav-bar.component';
import { FooterComponent } from './footer/footer.component';
import { JwtModule } from '@auth0/angular-jwt';


export function tokenGetter() {
  return localStorage.getItem("jwt");
}
import { FilterationModule } from 'src/filteration/filteration.module';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

import { DropDownListModule } from '@syncfusion/ej2-angular-dropdowns';
import { UserprofileComponent } from './userprofile/userprofile.component';
import { DelAccountComponent } from './del-account/del-account.component';
import { ReviewComponent } from './review/review.component';
import { AdDetailsComponent } from './ad-details/ad-details.component';

//signalr
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MyAdsComponent } from './my-ads/my-ads.component';
@NgModule({
  declarations: [
    AppComponent,
    UserprofileComponent,
    DelAccountComponent,
    HomePageComponentComponent,

    NavBarComponent,
    CatNavBarComponent,
    FooterComponent,
    ReviewComponent,
    AdDetailsComponent,
    MyAdsComponent,
    

  ],
  imports: [
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    DropDownListModule,
    AppRoutingModule,
    HttpClientModule,
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
