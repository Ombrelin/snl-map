import {ComponentFixture, TestBed} from '@angular/core/testing';

import {CityDetailComponent} from './city-detail.component';
import {DefaultCityService} from "../../services/implementations/DefaultCityService";
import {HttpClientModule} from "@angular/common/http";

describe('CityDetailComponent', () => {
  let component: CityDetailComponent;
  let fixture: ComponentFixture<CityDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CityDetailComponent],
      imports: [
        HttpClientModule
      ],
      providers: [
        {
          provide: "CityService",
          useClass: DefaultCityService
        }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CityDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
