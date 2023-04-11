import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EarthquakeService {
  private lastEarthquakeRomaniaUrl =
    'https://localhost:7067/api/earthquakes/latest-earthquake-romania';

  constructor() {}
}
