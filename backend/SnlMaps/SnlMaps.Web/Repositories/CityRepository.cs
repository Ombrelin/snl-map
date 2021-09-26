using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using SnlMaps.Domain;
using SnlMaps.Repositories;
using SnlMaps.Web.Database;

namespace SnlMaps.Web.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CityRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<City>> GetCitiesWithSruDeficit()
        {
            return this.dbContext
                .Cities
                .Where(city => city.SruDeficit)
                .ToListAsync();
        }

        public Task<City> GetCityFromCoord(Point location)
        {
            return this.dbContext
                .Cities
                .Where(city => city.Geometry.Contains(location))
                .FirstAsync();
        }

        public Task<City> GetCityFromName(string name)
        {
            return this.dbContext
                .Cities
                .FirstAsync(city => city.Name == name);
        }

        public Task<List<City>> GetCities()
        {
            return this.dbContext
                .Cities
                .ToListAsync();
        }

        public Task InsertCities(List<City> cities)
        {
            foreach (var city in cities)
            {
                this.dbContext.Cities.Add(city);
            }

            return this.dbContext.SaveChangesAsync();
        }

        public Task<List<string>> SearchCityByName(string keyword)
        {
            return this.dbContext
                .Cities
                .Where(city => city.Name.ToLower().Contains(keyword.ToLower()))
                .Select(city => city.Name)
                .ToListAsync();
        }
    }
}