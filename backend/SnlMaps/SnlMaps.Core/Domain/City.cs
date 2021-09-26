using NetTopologySuite.Geometries;

namespace SnlMaps.Domain
{
    public class City
    {
        public string Name { get; set; }
        public string InseeCode { get; set; }
        public int Population { get; set; }
        public Polygon Geometry { get; set; }
        public string PostCode { get; set; }
        public Point Location { get; set; }
        public bool SruDeficit { get; set; }
        public decimal SocialHousingRate { get; set; }
        public int SocialHousingCount { get; set; }
        public int SnlHousingCount { get; set; }
    }
}