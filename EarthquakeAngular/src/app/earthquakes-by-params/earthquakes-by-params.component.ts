import { Component } from '@angular/core';
import { EarthquakeResponse } from '../models/earthquake-response';
import { EarthquakeService } from '../services/earthquake.service';
import { EarthquakeRequest } from '../models/earthquakes-request';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject, filter, takeUntil } from 'rxjs';

@Component({
  selector: 'app-earthquakes-by-params',
  templateUrl: './earthquakes-by-params.component.html',
  styleUrls: ['./earthquakes-by-params.component.scss'],
})
export class EarthquakesByParamsComponent {
  pageTitle = 'Earthquakes by params';

  earthquakesToDisplay: EarthquakeResponse[];
  errorMessage: any;
  earthquakeForm: FormGroup;
  private unsubscribe$ = new Subject();

  initializeRequest() {

    this.earthquakeForm = this.fb.group({
      start: [null, [Validators.required]],
      end: [null, [Validators.required]],
      magnitude: [null],
      orderBy: [null],
    });

    this.earthquakeForm.valueChanges
      .pipe(
        filter(() => !this.earthquakeForm.pristine),
        takeUntil(this.unsubscribe$)
      )
      .subscribe(() => {
        const startTime = this.getSartTime?.value;
        const endTime = this.getEndTime?.value;

        if (startTime && endTime) {
          this.earthquakeRequest.startTime = new Date(startTime);
          this.earthquakeRequest.endTime = new Date(endTime);
        }

        this.earthquakeRequest.maxMagnitude = this.getMagnitude?.value;
        this.earthquakeRequest.orderBy = this.getOrderBy?.value;
      });
  }

  earthquakeRequest: EarthquakeRequest = {
    startTime: new Date(),
    endTime: new Date(),
    maxMagnitude: 5,
    orderBy: '',
  };

  constructor(
    private earthquakeService: EarthquakeService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initializeRequest();
  }

  ngOnDestroy() {
    this.unsubscribe$.next;
  }

  getEarthquakes() {
    if (this.earthquakeForm.valid) {
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

  clickEarthquake(earthquake: EarthquakeResponse) {
    this.earthquakeService.updateEarthquake(earthquake);
  }

  get getSartTime() {
    return this.earthquakeForm.get('start');
  }

  get getEndTime() {
    return this.earthquakeForm.get('end');
  }

  get getMagnitude() {
    return this.earthquakeForm.get('magntiude');
  }

  get getOrderBy() {
    return this.earthquakeForm.get('orderBy');
  }
}
