import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EarthquakeRomaniaComponent } from './earthquake-romania/earthquake-romania.component';
import { HttpClientModule } from '@angular/common/http';
import { EarthquakeService } from './services/earthquake.service';
import { WelcomeComponent } from './welcome/welcome.component';

@NgModule({
  declarations: [AppComponent, EarthquakeRomaniaComponent, WelcomeComponent],
  imports: [BrowserModule, AppRoutingModule, HttpClientModule],
  providers: [EarthquakeService],
  bootstrap: [AppComponent],
})
export class AppModule {}
