import { Component } from '@angular/core';
import { EarthquakeService } from '../services/earthquake.service';
import { Earthquake } from '../models/earthquake';

@Component({
  selector: 'app-earthquake-romania',
  templateUrl: './earthquake-romania.component.html',
  styleUrls: ['./earthquake-romania.component.scss'],
})
export class EarthquakeRomaniaComponent {
  pageTitle: string = 'Last earthquake from Romania';
  earthquake: Earthquake | undefined;

  constructor(private earthquakeService: EarthquakeService) {}

  ngOnInit(): void {
    this.getEarthquake();
  }

  getEarthquake() {
    this.earthquakeService.getLastEarthquakeFromRomania().subscribe((data) => {
      this.earthquake = data;
    });
  }
}
