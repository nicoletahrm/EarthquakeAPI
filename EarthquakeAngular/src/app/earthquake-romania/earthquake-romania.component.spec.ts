import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EarthquakeRomaniaComponent } from './earthquake-romania.component';

describe('EarthquakeListComponent', () => {
  let component: EarthquakeRomaniaComponent;
  let fixture: ComponentFixture<EarthquakeRomaniaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EarthquakeRomaniaComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EarthquakeRomaniaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
