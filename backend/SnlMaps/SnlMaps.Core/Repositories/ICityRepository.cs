using System.Collections.Generic;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using SnlMaps.Domain;

namespace SnlMaps.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetCitiesWithSruDeficit();
        Task<City> GetCityFromCoord(Point location);
        Task<City> GetCityFromName(string name);
        Task<List<City>> GetCities();
        Task InsertCities(List<City> cities);
        Task<List<string>> SearchCityByName(string keyword);
    }
}