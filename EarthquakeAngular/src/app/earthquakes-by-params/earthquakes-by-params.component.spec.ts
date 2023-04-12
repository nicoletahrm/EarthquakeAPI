import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EarthquakesByParamsComponent } from './earthquakes-by-params.component';

describe('EarthquakesByParamsComponent', () => {
  let component: EarthquakesByParamsComponent;
  let fixture: ComponentFixture<EarthquakesByParamsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EarthquakesByParamsComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EarthquakesByParamsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
