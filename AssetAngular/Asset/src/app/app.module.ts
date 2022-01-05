import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {NgxPaginationModule} from 'ngx-pagination';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HomeService } from './home/home.service';

import { HttpClientModule } from '@angular/common/http';
import { AddEditComponent } from './home/add-edit/add-edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddeditService } from './services/addedit.service';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddEditComponent
  ],
  imports: [
    BrowserModule,
    NgxPaginationModule,


    AppRoutingModule,
    HttpClientModule,
    FormsModule, 
    ReactiveFormsModule
  ],
  providers: [HomeService,AddeditService],
  bootstrap: [AppComponent]
})
export class AppModule { }
