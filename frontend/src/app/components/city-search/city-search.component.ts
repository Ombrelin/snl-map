import {Component, EventEmitter, Inject, Output} from '@angular/core';
import {CityService} from "../../core/services/CityService";
import {CommandAsync} from "@ssv/ngx.command";
import {BehaviorSubject} from "rxjs";
import {City} from "../../core/model/City";

@Component({
  selector: 'app-city-search',
  templateUrl: './city-search.component.html',
  styleUrls: ['./city-search.component.scss']
})
export class CitySearchComponent {
  public searchResult: Array<string> = [];
  public keyword = "";
  @Output() searchCityEvent = new EventEmitter<City>();
  public searchCityCommand = new CommandAsync(async () =>
      await this.searchCityDetail(),
    new BehaviorSubject(true)
  );
  public fetchCityCommand = new CommandAsync(async () =>
      await this.getCityInfos(),
    new BehaviorSubject(true)
  );
  private searchThrottlingTimeout: number | undefined;

  constructor(
    @Inject('CityService') private cityService: CityService
  ) {
  }

  get searchButtonEnabled(): boolean {
    return !!this.keyword && this.keyword != "";
  }

  handleChange(event: Event) {
    this.keyword = (event.target as HTMLInputElement).value;
    if (this.searchThrottlingTimeout) {
      clearTimeout(this.searchThrottlingTimeout)
    }
    this.searchThrottlingTimeout = setTimeout(() => {
      this.searchCityCommand.execute();
    }, 300);
  }

  private async searchCityDetail() {
    this.searchResult = await this.cityService.searchCity(this.keyword, 5);
  }

  private async getCityInfos() {
    const city:City = await this.cityService.getCityFromName(this.keyword);
    console.log(city);
    this.searchCityEvent.emit(city);
  }
}
