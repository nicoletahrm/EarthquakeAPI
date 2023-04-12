import { Component } from '@angular/core';
import { EarthquakeService } from '../services/earthquake.service';
import { EarthquakeResponse } from '../models/earthquake-response';

@Component({
  selector: 'app-earthquake-romania',
  templateUrl: './earthquake-romania.component.html',
  styleUrls: ['./earthquake-romania.component.scss'],
})
export class EarthquakeRomaniaComponent {
  pageTitle: string = 'Last earthquake from Romania';
  earthquakeResponse: EarthquakeResponse | undefined;

  constructor(private earthquakeService: EarthquakeService) {}

  ngOnInit(): void {
    this.getEarthquake();
  }

  getEarthquake() {
    this.earthquakeService.getLastEarthquakeFromRomania().subscribe((data) => {
      this.earthquakeResponse = data;
    });
  }
}
