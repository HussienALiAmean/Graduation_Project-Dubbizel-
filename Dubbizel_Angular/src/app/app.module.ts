import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http'

import { DropDownListModule } from '@syncfusion/ej2-angular-dropdowns';
import { UserprofileComponent } from './userprofile/userprofile.component';
import { DelAccountComponent } from './del-account/del-account.component';
import { AddAdvComponent } from './add-adv/add-adv.component';
@NgModule({
  declarations: [
    AppComponent,
    UserprofileComponent,
    DelAccountComponent,
    AddAdvComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    DropDownListModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
