using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using SnlMaps.Domain;
using SnlMaps.Repositories;
using SnlMaps.Web.Controllers;
using Xunit;

namespace SnlMaps.Web.Tests
{
    public class CitiesControllerTests
    {
        [Fact]
        public async Task InsertCities_FromCsv_InsertRecords()
        {
            // Given
            List<City> cities = null;
            Mock<ICityRepository> repository = new Mock<ICityRepository>();
            repository
                .Setup(mock => mock.InsertCities(It.IsAny<List<City>>()))
                .Returns<List<City>>(citiesResult =>
                {
                    cities = citiesResult;
                    return Task.CompletedTask;
                });

            var citiesController = new CitiesController(repository.Object);
            using var streamReader = new StreamReader("cities.csv");
            var stream = streamReader.BaseStream;

            // When
            await citiesController.InsertCities(new FormFile(stream, 0, stream.Length, "cities", "cities.csv"));

            // Then
            cities.Should().ContainSingle();
            cities[0].Name.Should().Be("Paris");
            cities[0].InseeCode.Should().Be("75056");
            cities[0].Population.Should().Be(2190327);
            cities[0].PostCode.Should()
                .Be(
                    "75001,75002,75003,75004,75005,75006,75007,75008,75009,75010,75011,75012,75013,75014,75015,75016,75017,75018,75019,75020");
            cities[0].Geometry.Coordinates.Should().HaveCount(414);
            cities[0].Location.X.Should().Be(48.845);
            cities[0].Location.Y.Should().Be(2.3752);
            cities[0].SruDeficit.Should().Be(true);
            cities[0].SocialHousingRate.Should().Be(21.38M);
            cities[0].SocialHousingCount.Should().Be(250618);
            cities[0].SnlHousingCount.Should().Be(228);
        }

        [Fact]
        public async Task GetCitiesWithSruDeficit_ReturnsResultFromRepo()
        {
            // Given
            var (paris, vitry) = DataFactory.BuildSimpleCities();
            Mock<ICityRepository> repository = new Mock<ICityRepository>();
            repository.Setup(mock => mock.GetCitiesWithSruDeficit())
                .Returns(() => Task.FromResult(new List<City>() { paris, vitry }));

            var citiesController = new CitiesController(repository.Object);

            // When
            var result = await citiesController.GetCitiesWithSruDeficit();

            // Then
            result.Value.Count.Should().Be(2);
        }
    }
}