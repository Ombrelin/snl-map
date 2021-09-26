import {City} from "../model/City";

export interface CityService {
  getCitiesWithSruDeficit(): Promise<Array<City>>

  getCityFromCoord(latitude: number, longitude: number): Promise<City>

  getCityFromName(name: String): Promise<City>

  searchCity(keyword: string, count: number): Promise<Array<string>>
}
