import {CityService} from "../../core/services/CityService";
import {City} from "../../core/model/City";
import {Injectable} from "@angular/core";
import CitiesApiClient, {createClient} from "./CitiesApiClient";

@Injectable()
export class DefaultCityService implements CityService {

  private client = createClient();

  async getCitiesWithSruDeficit(): Promise<Array<City>> {
    const response = await this.client.getCitiesWithSruDeficit();
    return response.data;
  }

  async getCityFromCoord(latitude: number, longitude: number): Promise<City> {
    const response = await this.client.getCityFromCoord(latitude, longitude);
    return response.data;
  }

  async getCityFromName(name: String): Promise<City> {
    const response = await this.client.getCityFromName(name);
    return response.data;
  }

  async searchCity(keyword: string, count: number): Promise<Array<string>> {
    const response = await this.client.searchCity(keyword);
    return response.data;
  }


}

