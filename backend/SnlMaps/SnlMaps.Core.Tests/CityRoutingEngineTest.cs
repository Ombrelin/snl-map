using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using SnlMaps.Domain;
using Xunit;

namespace SnlMaps.Core.Tests
{
    public class CityRoutingEngineTest
    {
        [Fact]
        public async Task ComputeRouteToAgglomerationCenter_ReturnsRouteFromNavitia()
        {
            // Given
            Environment.SetEnvironmentVariable("NAVITIA_TOKEN","mock-token");

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(mock => mock.CreateClient())
                .Returns(() => new HttpClient(new FakeHttpClientHandler()));
            
            var engine = new CityRoutingEngine(httpClientFactoryMock.Object);
        }
    }
}