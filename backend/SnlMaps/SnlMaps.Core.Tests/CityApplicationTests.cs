using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NetTopologySuite.Geometries;
using SnlMaps.Applications;
using SnlMaps.Domain;
using SnlMaps.Repositories;
using Xunit;

namespace SnlMaps.Core.Tests
{
    public class CityApplicationTests
    {
        [Fact]
        public async Task GetCitiesWithSruDeficit_returnsDataFromRepository()
        {
            // Given
            Mock<ICityRepository> repository = new Mock<ICityRepository>();
            repository.Setup(mock => mock.GetCitiesWithSruDeficit())
                .Returns(() => Task.FromResult(new List<City>
                    {
                        new City()
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
                        }
                    }
                ));

            ICityApplication application = new CityApplication(repository.Object);

            // When
            List<City> cities = await application.GetCitiesWithSruDeficit();

            // Then
            cities.Should().ContainSingle();
            cities[0].Name.Should().Be("Paris");
        }

        [Fact]
        public async Task GetCityFromCoord()
        {
            // Given
            Mock<ICityRepository> repository = new Mock<ICityRepository>();
            ICityApplication application = new CityApplication(repository.Object);

            // When

            // Then
        }

        [Fact]
        public async Task GetCityFromName()
        {
            // Given
            Mock<ICityRepository> repository = new Mock<ICityRepository>();
            ICityApplication application = new CityApplication(repository.Object);

            // When

            // Then
        }

        [Fact]
        public async Task SearchCity()
        {
            // Given
            Mock<ICityRepository> repository = new Mock<ICityRepository>();
            ICityApplication application = new CityApplication(repository.Object);

            // When

            // Then
        }

        [Fact]
        public async Task InsertCities()
        {
            // Given
            var cities = new List<City>
            {
                new City()
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
                }
            };
            
            Mock<ICityRepository> repository = new Mock<ICityRepository>();
            repository.Setup(mock => mock.InsertCities(It.IsAny<List<City>>()))
                .Returns<List<City>>(citiesParam =>
                {
                    citiesParam.Should().ContainSingle();
                    citiesParam[0].Name.Should().Be("Paris");
                    return Task.CompletedTask;
                });
            
            ICityApplication application = new CityApplication(repository.Object);
            
            // When
            await application.InsertCities(cities);
        }
    }
}