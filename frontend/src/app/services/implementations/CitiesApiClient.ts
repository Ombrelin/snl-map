import {BasePath, BaseService, GET, Path, Query, Response, ServiceBuilder} from "ts-retrofit";
import {City} from "../../core/model/City";
import {API_URL} from "../constants";
import axios from 'axios';

axios.defaults.withCredentials = false;
axios.defaults.validateStatus = () => {
  return true;
};

@BasePath("/cities")
export default class CitiesApiClient extends BaseService {
  @GET("/sru")
  async getCitiesWithSruDeficit(): Promise<Response<Array<City>>> {
    return <Response<Array<City>>>{};
  }

  @GET("/locate")
  async getCityFromCoord(@Query("lat") latitude: number, @Query("long") longitude: number): Promise<Response<City>> {
    return <Response<City>>{};
  }

  @GET("/{name}")
  async getCityFromName(@Path("name") name: String): Promise<Response<City>> {
    return <Response<City>>{};
  }

  @GET("/search")
  async searchCity(@Query("keyword") keyword: string): Promise<Response<Array<string>>> {
    return <Response<Array<string>>>{};
  }
}

const createClient = (): CitiesApiClient => new ServiceBuilder()
  .setEndpoint(API_URL)
  .setTimeout(10000)
  .build(CitiesApiClient);

export {createClient};
