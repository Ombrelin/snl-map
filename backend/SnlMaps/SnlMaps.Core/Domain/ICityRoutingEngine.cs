using System.Threading.Tasks;

namespace SnlMaps.Domain
{
    public interface ICityRoutingEngine
    {
        Task<Route> ComputeRouteToAgglomerationCenter(City city);
    }
}