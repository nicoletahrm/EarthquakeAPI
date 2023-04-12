import { Component } from '@angular/core';
import { EarthquakeResponse } from '../models/earthquake-response';
import { ActivatedRoute, Router } from '@angular/router';
import { EarthquakeService } from '../services/earthquake.service';
import { EarthquakeRequest } from '../models/earthquakes-request';

@Component({
  selector: 'app-earthquake-detail',
  templateUrl: './earthquake-detail.component.html',
  styleUrls: ['./earthquake-detail.component.scss'],
})
export class EarthquakeDetailComponent {
  pageTitle: string = 'Earthquake detail';
  earthquake: EarthquakeResponse;
  errorMessage = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private earthquakeService: EarthquakeService
  ) {}

  ngOnInit(): void {
    const id = String(this.route.snapshot.paramMap.get('id'));
    if (id) {
      //this.getEarthquake(id);
    }
    this.earthquakeService.currentEarthQuake.subscribe(
      (result) => (this.earthquake = result)
    );
  }

  getEarthquake(id: string): void {
    this.earthquakeService.getEarthquakeById(id).subscribe({
      next: (earthquake) => (this.earthquake = this.earthquake),
      error: (err) => (this.errorMessage = err),
    });
  }

  onBack(): void {
    this.router.navigate(['/welcome']);
  }
}
