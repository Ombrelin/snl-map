import {Component, Inject, Input, OnInit} from '@angular/core';
import {City} from "../../core/model/City";
import {LatLng} from "leaflet";
import {CommandAsync} from "@ssv/ngx.command";
import {BehaviorSubject} from "rxjs";
import {CityService} from "../../core/services/CityService";

@Component({
  selector: 'app-city-detail',
  templateUrl: './city-detail.component.html',
  styleUrls: ['./city-detail.component.scss']
})
export class CityDetailComponent implements OnInit {

  public city: City | undefined;
  public fetchCityDetailsByCoordCommand = new CommandAsync(async () =>
      await this.fetchCityDetails(),
    new BehaviorSubject(true)
  );

  constructor(
    @Inject('CityService') private cityService: CityService
  ) {
  }

  private _coordinates: LatLng = new LatLng(0, 0);

  @Input() set coordinates(value: LatLng | undefined) {
    if (value == undefined) {
      return;
    }
    this._coordinates = value;
    this.fetchCityDetailsByCoordCommand.execute();
  }

  @Input() set selectedCity(value: City | undefined) {
    if (value == undefined) {
      return;
    }
    this.city = value;
  }

  ngOnInit(): void {
  }

  private async fetchCityDetails() {
    this.city = undefined;
    this.city = await this.cityService.getCityFromCoord(this._coordinates.lat, this._coordinates.lng);
  }
}
