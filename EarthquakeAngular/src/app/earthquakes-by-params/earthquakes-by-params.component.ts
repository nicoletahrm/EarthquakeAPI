import { Component } from '@angular/core';
import { EarthquakeResponse } from '../models/earthquake-response';
import { EarthquakeService } from '../services/earthquake.service';
import { EarthquakeRequest } from '../models/earthquakes-request';
import {
  AbstractControl,
  FormBuilder,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Subject, filter, startWith, takeUntil } from 'rxjs';

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
  message: String = 'Invalid date';
  message1: String = 'StartDate  > EndDate';

  items = ['', 'time', 'time-asc', 'magnitude', 'magnitude-asc'];
  selectedItem = this.items[0];

  initializeRequest() {
    this.earthquakeForm = this.fb.group(
      {
        start: [null, [Validators.required, this.dateValidator]],
        end: [null, [Validators.required, this.dateValidator]],
        magnitude: [null],
        orderBy: [this.items[0]],
      },
      { validator: this.startDateAfterEndDate }
    );

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

  dateValidator: ValidatorFn = (
    control: AbstractControl
  ): { [key: string]: any } | null => {
    let date = control.value;

    if (date === '' || date == null) {
      return null;
    }

    const selectedDate = new Date(control.value);
    const currentDate = new Date();

    if (selectedDate > currentDate) {
      return { dateInFuture: true };
    }

    return null;
  };

  startDateAfterEndDate: ValidatorFn = (
    control: AbstractControl
  ): { [key: string]: any } | null => {
    const start = control.get('start')?.value;
    const end = control.get('end')?.value;

    if (start >= end) {
      return { startError: true };
    }

    return null;
  };
}
