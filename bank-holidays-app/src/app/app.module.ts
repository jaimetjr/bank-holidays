import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SearchGridComponent } from './components/search-grid/search-grid.component';
import { SearchComponent } from './components/search/search.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './shared/header/header.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    SearchComponent,
    SearchGridComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
