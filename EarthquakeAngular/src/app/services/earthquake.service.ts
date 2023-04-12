import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Observable, catchError, map, tap, throwError } from 'rxjs';
import { Earthquake } from '../models/earthquake';

@Injectable({
  providedIn: 'root',
})
export class EarthquakeService {
  private lastEarthquakeRomaniaUrl =
    'https://localhost:7067/api/earthquakes/latest-earthquake-romania';

  constructor(private http: HttpClient) {}

  getLastEarthquakeFromRomania(): Observable<Earthquake> {
    return this.http.get<Earthquake>(this.lastEarthquakeRomaniaUrl).pipe(
      tap((data) =>
        console.log('Latest Earthquake in Romania: ' + JSON.stringify(data))
      ),
      catchError(this.handleError)
    );
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
