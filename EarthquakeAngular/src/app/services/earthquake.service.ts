import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Observable, catchError, map, of, tap, throwError } from 'rxjs';
import { EarthquakeResponse } from '../models/earthquake-response';
import { EarthquakeRequest } from '../models/earthquakes-request';

@Injectable({
  providedIn: 'root',
})
export class EarthquakeService {
  lastEarthquakeRomaniaUrl =
    'https://localhost:7067/api/earthquakes/latest-earthquake-romania';

  earthquakesWithParamsUrl =
    'https://localhost:7067/api/earthquakes/earthquakes-by-params';

  earthquakesResponse: Observable<EarthquakeResponse[]> | undefined;

  constructor(private http: HttpClient) {}

  getLastEarthquakeFromRomania(): Observable<EarthquakeResponse> {
    return this.http
      .get<EarthquakeResponse>(this.lastEarthquakeRomaniaUrl)
      .pipe(
        tap((data) =>
          console.log('Latest Earthquake in Romania: ' + JSON.stringify(data))
        ),
        catchError(this.handleError)
      );
  }

  getEarthquakesByParams(
    earthquakeRequest: EarthquakeRequest
  ): Observable<EarthquakeResponse[]> {
    const params = new HttpParams()
      .set('startTime', new Date(earthquakeRequest.startTime).toISOString())
      .set('endTime', new Date(earthquakeRequest.endTime).toISOString())
      .set('maxMagnitude', earthquakeRequest.maxMagnitude?.toString() || '')
      .set('orderBy', earthquakeRequest.orderBy || '');

    this.earthquakesResponse = this.http.get<EarthquakeResponse[]>(
      this.earthquakesWithParamsUrl,
      {
        params: params,
      }
    );

    return this.earthquakesResponse;
  }

  getEarthquakeById(id: string): Observable<EarthquakeResponse | undefined> {
    if (this.earthquakesResponse) {
      return this.earthquakesResponse.pipe(
        map((earthquakes: EarthquakeResponse[]) =>
          earthquakes.find((e) => e.id === id.toString())
        )
      );
    }
    return of(undefined);
  }

  private handleError(err: HttpErrorResponse): Observable<never> {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';

    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }

    console.error(errorMessage);

    return throwError(() => errorMessage);
  }
}
