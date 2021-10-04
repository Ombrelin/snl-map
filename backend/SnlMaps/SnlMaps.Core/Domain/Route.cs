using System;
using NetTopologySuite.Geometries;

namespace SnlMaps.Domain
{
    public record Route(
        TimeSpan Duration,
        LineString[] Path
    );
}