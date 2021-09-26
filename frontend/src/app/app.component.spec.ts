import {TestBed} from '@angular/core/testing';
import {AppComponent} from './app.component';
import {CoreModule} from "./core/core.module";
import {ServicesModule} from "./services/services.module";
import {HttpClientModule} from "@angular/common/http";
import {DefaultCityService} from "./services/implementations/DefaultCityService";
import {MatInputModule} from "@angular/material/input";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatAutocompleteModule} from "@angular/material/autocomplete";

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        AppComponent
      ],
      imports: [
        CoreModule,
        ServicesModule,
        HttpClientModule,
        MatInputModule,
        MatFormFieldModule,
        MatAutocompleteModule
      ], providers: [
        {
          provide: "CityService",
          useClass: DefaultCityService
        }
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

});
