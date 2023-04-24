import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EarthquakeRomaniaComponent } from './earthquake-romania/earthquake-romania.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { EarthquakesByParamsComponent } from './earthquakes-by-params/earthquakes-by-params.component';
import { EarthquakeDetailComponent } from './earthquake-detail/earthquake-detail.component';

const routes: Routes = [
  { path: 'welcome', component: WelcomeComponent },
  { path: 'earthquake-romania', component: EarthquakeRomaniaComponent },
  { path: 'earthquake-by-params', component: EarthquakesByParamsComponent },
  {
    path: 'earthquake-by-params/:id',
    component: EarthquakeDetailComponent,
  },
  { path: '', redirectTo: 'welcome', pathMatch: 'full' },
  { path: '**', redirectTo: 'welcome', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
