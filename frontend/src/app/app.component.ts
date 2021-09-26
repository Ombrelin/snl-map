import {Component, Inject} from '@angular/core';
import {geoJSON, GeoJSONOptions, icon, latLng, LatLng, Layer, LeafletMouseEvent, Map, marker, tileLayer} from "leaflet";
import {CityService} from "./core/services/CityService";
import {Point} from "geojson";
import {City} from "./core/model/City";
import {MatDialog} from "@angular/material/dialog";
import {AboutModalComponent} from "./components/about-modal/about-modal.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public selectedCoordinates: LatLng | undefined;
  public selectedCityName: string | undefined;
  public selectedCity: City | undefined;
  layers: Array<Layer> = [
    geoJSON(({
        "type": "Polygon",
        "coordinates": [[[2.929582217839, 48.653032178059], [2.9142438732182, 48.652433867818], [2.9095084731644, 48.660401539629], [2.9028999428545, 48.664921589525], [2.8989476006264, 48.674512930067], [2.8961013839408, 48.681124227367], [2.8960504211898, 48.693094128306], [2.9075686055332, 48.69475769818], [2.914617784581, 48.701988202328], [2.9213077265946, 48.699079502892], [2.9263455243884, 48.690793402444], [2.937631528459, 48.687023934531], [2.9466939405061, 48.686932987393], [2.9560019141513, 48.679112405262], [2.9735205879089, 48.671366908502], [2.974238795694, 48.669025753311], [2.9699188920858, 48.659780874241], [2.9551970132488, 48.661036413429], [2.9430064790365, 48.668785548836], [2.9299898477583, 48.657839230918], [2.929582217839, 48.653032178059]]]
      }) as any
    )
  ]
  options = {
    layers: [
      tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {maxZoom: 18, attribution: 'Open Street Map'})
    ],
    zoom: 12,
    center: latLng(48.8534, 2.3488)
  };
  private leafletMap: Map | undefined;

  constructor(
    @Inject('CityService') private cityService: CityService,
    private dialogService: MatDialog
  ) {
  }

  async onMapReady(map: Map) {
    this.leafletMap = map;
    await this.loadData();
  }

  async loadData() {
    const sruCities: Array<City> = await this.cityService.getCitiesWithSruDeficit()


    const options: GeoJSONOptions = {style: {color: "#D50000"}};
    this.layers = [geoJSON(sruCities.map(city => city.geometry) as any, options)];
  }

  async onMapClick(event: LeafletMouseEvent) {
    const latLng = event.latlng;
    this.setMarker(latLng);
    this.selectedCoordinates = latLng;
  }

  public handleSearchCityByName(city: City) {
    this.selectedCity = city;
    console.log("city",this.selectedCity);
    console.log("location",this.selectedCity.location);
    console.log("coord",this.selectedCity.location.coordinates);
    const [longitude, latitude] = city.location.coordinates;
    const position = new LatLng(longitude, latitude);
    this.setMarker(position);

    this.leafletMap?.setView(position, 13);
  }

  handleClickAbout() {
    this.dialogService.open(AboutModalComponent, {
      width: "800px"
    });
  }

  private setMarker(latLng: LatLng) {
    if (this.mapHasMarker()) {
      this.layers.pop();
    }

    const newMarker = marker(latLng, {
      icon: icon({
        iconSize: [25, 41],
        popupAnchor: [1, -34],
        iconUrl: 'assets/marker-icon.png'
      })
    });
    this.layers.push(newMarker);
  }

  private mapHasMarker() {
    return this.layers.length > 1;
  }
}
