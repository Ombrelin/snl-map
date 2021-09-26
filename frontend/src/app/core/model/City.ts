import {Point, Polygon} from "geojson";

export interface City {
  name: string,
  inseeCode: string,
  population: number,
  geometry: Polygon,
  postCode: string,
  location: Point,
  sruDeficit: boolean,
  socialHousingRate: number,
  socialHousingCount: number,
  snlHousingCount: number
}
