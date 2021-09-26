import {CommonModule} from "@angular/common";
import {NgModule} from "@angular/core";
import {DefaultCityService} from "./implementations/DefaultCityService";
import {createClient} from "./implementations/CitiesApiClient";

@NgModule({
    imports: [
      CommonModule
    ],
    providers:
      [
        {
          provide: "CityService",
          useClass: DefaultCityService
        }
      ]
  }
)
export class ServicesModule {

}
