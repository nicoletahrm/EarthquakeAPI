import { Component } from '@angular/core';
import { EarthquakeResponse } from '../models/earthquake-response';
import { EarthquakeService } from '../services/earthquake.service';
import { EarthquakeRequest } from '../models/earthquakes-request';

@Component({
  selector: 'app-earthquakes-by-params',
  templateUrl: './earthquakes-by-params.component.html',
  styleUrls: ['./earthquakes-by-params.component.scss'],
})
export class EarthquakesByParamsComponent {
  pageTitle = 'Earthquakes by params';

  earthquakesToDisplay: EarthquakeResponse[] = [];
  errorMessage: any;
  start: Date = new Date();
  end: Date = new Date();
  maxMagnitude: number = 5;
  orderBy: string = 'time';

  earthquakeRequest: EarthquakeRequest = {
    startTime: new Date(),
    endTime: new Date(),
    maxMagnitude: 5,
    orderBy: '',
  };

  constructor(private earthquakeService: EarthquakeService) {}

  ngOnInit(): void {}

  searchEarthquakes() {
    this.earthquakeRequest.startTime = this.start;
    this.earthquakeRequest.endTime = this.end;
    this.earthquakeRequest.maxMagnitude = this.maxMagnitude;
    this.earthquakeRequest.orderBy = this.orderBy;

    this.earthquakeService
      .getEarthquakesByParams(this.earthquakeRequest)
      .subscribe({
        next: (earthquakes) => {
          this.earthquakesToDisplay = earthquakes;
        },
        error: (err) => (this.errorMessage = err),
      });
  }
}
