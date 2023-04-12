import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EarthquakeRomaniaComponent } from './earthquake-romania/earthquake-romania.component';
import { HttpClientModule } from '@angular/common/http';
import { EarthquakeService } from './services/earthquake.service';
import { WelcomeComponent } from './welcome/welcome.component';
import { EarthquakesByParamsComponent } from './earthquakes-by-params/earthquakes-by-params.component';
import { FormsModule } from '@angular/forms';
import { EarthquakeDetailComponent } from './earthquake-detail/earthquake-detail.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    EarthquakeRomaniaComponent,
    WelcomeComponent,
    EarthquakesByParamsComponent,
    EarthquakeDetailComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule,
  ],
  providers: [EarthquakeService],
  bootstrap: [AppComponent],
})
export class AppModule {}
