// Http testing module and mocking controller
import {HttpClientTestingModule} from '@angular/common/http/testing';

// Other imports
import {TestBed} from '@angular/core/testing';
import {HttpClientModule} from '@angular/common/http';
import {DefaultCityService} from "./DefaultCityService";

describe("Default city service", () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, HttpClientModule],
      providers: [DefaultCityService]
    });
  });
})
