export interface EarthquakeRequest {
  startTime: Date;
  endTime: Date;
  maxMagnitude: number;
  orderBy: string;
}
