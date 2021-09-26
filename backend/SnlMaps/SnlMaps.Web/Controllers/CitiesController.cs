using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using SnlMaps.Domain;
using SnlMaps.Repositories;

namespace SnlMaps.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository repository;

        public CitiesController(ICityRepository repository)
        {
            this.repository = repository;
        }


        /// <summary>
        /// Bulk import of cities from CSV file
        /// </summary>
        /// <param name="formFile">CSV file as form data</param>
        [HttpPost]
        [Authorize]
        public async Task InsertCities(IFormFile formFile)
        {
            using var stream = new StreamReader(formFile.OpenReadStream());
            var lines = (await stream.ReadToEndAsync())
                .Split("\n")
                .Select(line => line.Split(";"));

            List<City> cities = lines
                .Skip(1)
                .Where(lineData => lineData.Length == 11)
                .Select(BuildCityFromLine)
                .ToList();

            await repository.InsertCities(cities);
        }

        private static City BuildCityFromLine(string[] cityData)
        {
            var culture = CultureInfo.CreateSpecificCulture("fr-FR");
            return new City()
            {
                Name = cityData[0],
                InseeCode = cityData[1],
                Population = int.Parse(cityData[2]),
                Geometry = DeserializeGeometry(cityData[3]),
                PostCode = cityData[4],
                Location = new Point(double.Parse(cityData[5], NumberStyles.Float, culture),
                    double.Parse(cityData[6], NumberStyles.Float, culture)),
                SruDeficit = bool.Parse(cityData[7]),
                SocialHousingRate = decimal.Parse(cityData[8], NumberStyles.Float, culture),
                SocialHousingCount = int.Parse(cityData[9]),
                SnlHousingCount = int.Parse(cityData[10])
            };
        }

        private static Polygon DeserializeGeometry(string geoJson)
        {
            Polygon geometry;
            var serializer = GeoJsonSerializer.Create();
            using var stringReader = new StringReader(geoJson);
            using var jsonReader = new JsonTextReader(stringReader);
            geometry = serializer.Deserialize<Polygon>(jsonReader);

            return geometry;
        }

        [HttpGet("sru")]
        public async Task<ActionResult<List<City>>> GetCitiesWithSruDeficit()
        {
            return await this.repository.GetCitiesWithSruDeficit();
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCities()
        {
            return await this.repository.GetCities();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<City>> GetCityByName([FromRoute(Name = "name")] string name)
        {
            return await this.repository.GetCityFromName(name);
        }

        [HttpGet("locate")]
        public async Task<City> GetCityFromCoord([FromQuery(Name = "lat")] double latitude,
            [FromQuery(Name = "long")] double longitude)
        {
            return await this.repository.GetCityFromCoord(new Point(longitude, latitude) { SRID = 4326 });
        }

        [HttpGet("search")]
        public async Task<List<string>> SearchCityByName([FromQuery(Name = "keyword")] string keyword)

        {
            return await this.repository.SearchCityByName(keyword);
        }
    }
}