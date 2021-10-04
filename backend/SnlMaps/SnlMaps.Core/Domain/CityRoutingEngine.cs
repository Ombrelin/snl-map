using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SnlMaps.Domain
{
    public class CityRoutingEngine : ICityRoutingEngine
    {
        private static readonly string NAVITIA_TOKEN = Environment.GetEnvironmentVariable("NAVITIA_TOKEN");
        private readonly IHttpClientFactory httpClientFactory;

        public CityRoutingEngine(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public Task<Route> ComputeRouteToAgglomerationCenter(City city)
        {
            throw new System.NotImplementedException();
        }
    }
}