using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Npgsql;
using SnlMaps.Domain;
using SnlMaps.Web.Database;
using SnlMaps.Web.Repositories;
using Xunit;

namespace SnlMaps.Web.Tests
{
    public class CityRepositoryTests : IAsyncLifetime
    {
        private ApplicationDbContext context;

        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql(GetPostgresConnectionString(), x => x.UseNetTopologySuite())
                .Options;

            this.context = new ApplicationDbContext(options);

            await this.context.Database.MigrateAsync();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await context.Database.ExecuteSqlRawAsync("DELETE FROM \"Cities\"");
        }

        [Fact]
        public async Task InsertCities_InsertCitiesToDb()
        {
            // Given
            await context.Database.EnsureCreatedAsync();

            var repository = new CityRepository(context);
            var city = new City()
            {
                Name = "Paris",
                InseeCode = "75056",
                Population = 2190327,
                Geometry = new Polygon(new LinearRing(Array.Empty<Coordinate>())),
                Location = new Point(48.845, 2.3752),
                SruDeficit = true,
                SocialHousingRate = 21.38M,
                SocialHousingCount = 250618,
                SnlHousingCount = 228
            };

            // When 
            await repository.InsertCities(new List<City> { city });

            // Then
            Assert.Single(context.Cities);
            City createdCity = await context.Cities.FirstAsync();
            createdCity.Name.Should().Be("Paris");
        }

        [Fact]
        public async Task GetCitiesWithSruDeficit_ReturnsOnlyCitiesWithSruDeficit()
        {
            // Given
            await context.Database.EnsureCreatedAsync();

            var repository = new CityRepository(context);
            var (paris, vitry) = DataFactory.BuildSimpleCities();

            context.Cities.Add(paris);
            context.Cities.Add(vitry);
            await context.SaveChangesAsync();

            // When 
            List<City> result = await repository.GetCitiesWithSruDeficit();

            // Then
            Assert.Single(result);
            result[0].Name.Should().Be("Paris");
            result[0].InseeCode.Should().Be("75056");
        }


        [Fact]
        public async Task GetCities_ReturnsAllCities()
        {
            // Given
            await context.Database.EnsureCreatedAsync();

            var repository = new CityRepository(context);
            var (paris, vitry) = DataFactory.BuildSimpleCities();

            context.Cities.Add(paris);
            context.Cities.Add(vitry);
            await context.SaveChangesAsync();

            // When 
            List<City> result = await repository.GetCities();

            // Then
            result.Count.Should().Be(2);

            result[0].Name.Should().Be("Paris");
            result[0].InseeCode.Should().Be("75056");

            result[1].Name.Should().Be("Vitry-sur-Seine");
            result[1].InseeCode.Should().Be("94400");
        }

        [Fact]
        public async Task GetCitiesByName_ReturnsCityOfGivenName()
        {
            // Given
            await context.Database.EnsureCreatedAsync();

            var repository = new CityRepository(context);
            var (paris, vitry) = DataFactory.BuildSimpleCities();

            context.Cities.Add(paris);
            context.Cities.Add(vitry);
            await context.SaveChangesAsync();

            // When 
            City result = await repository.GetCityFromName("Vitry-sur-Seine");

            // Then
            result.Name.Should().Be("Vitry-sur-Seine");
            result.InseeCode.Should().Be("94400");

            await context.Database.ExecuteSqlRawAsync("DELETE FROM \"Cities\"");
        }

        [Fact]
        public async Task SearchCityByName_ReturnsNameOfAllCitiesWhoseNameContainsKeyWord()
        {
            // Given
            await context.Database.EnsureCreatedAsync();

            var repository = new CityRepository(context);
            var (paris, vitry) = DataFactory.BuildSimpleCities();

            context.Cities.Add(paris);
            context.Cities.Add(vitry);
            await context.SaveChangesAsync();

            // When 
            List<string> result = await repository.SearchCityByName("vi");

            // Thens
            Assert.Single(result);
            result.Should().Equal("Vitry-sur-Seine");
        }

        [Fact]
        public async Task GetCityFromCoord_ReturnsOnlyCitiesContainingTheCoord()
        {
            // Given
            await context.Database.EnsureCreatedAsync();

            var repository = new CityRepository(context);
            var paris = new City()
            {
                Name = "Paris",
                InseeCode = "75056",
                Geometry = new Polygon(new LinearRing(new Coordinate[]
                    { new(0, 0), new(1, 0), new(1, 1), new(0, 1), new(0, 0) }))
            };

            var vitry = new City()
            {
                Name = "Vitry-sur-Seine",
                InseeCode = "94400",
                Geometry = new Polygon(new LinearRing(new Coordinate[]
                    { new(1, 0), new(2, 0), new(2, 1), new(1, 1), new(1, 0) }))
            };

            context.Cities.Add(paris);
            context.Cities.Add(vitry);
            await context.SaveChangesAsync();

            // When 
            City result = await repository.GetCityFromCoord(new Point(1.5, 0.5));

            // Then
            result.Name.Should().Be("Vitry-sur-Seine");
            result.InseeCode.Should().Be("94400");
        }

        private string GetPostgresConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("TEST_DATABASE_URL");
            if (databaseUrl is null)
            {
                throw new ArgumentException("Please populate the TEST_DATABASE_URL env variable");
            }

            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/')
            };

            return builder.ToString();
        }
    }
}