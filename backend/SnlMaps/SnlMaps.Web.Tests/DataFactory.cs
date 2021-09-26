using SnlMaps.Domain;

namespace SnlMaps.Web.Tests
{
    public class DataFactory
    {
        public static (City, City) BuildSimpleCities()
        {
            var paris = new City()
            {
                Name = "Paris",
                InseeCode = "75056",
                SruDeficit = true
            };

            var vitry = new City()
            {
                Name = "Vitry-sur-Seine",
                InseeCode = "94400",
                SruDeficit = false
            };
            return (paris, vitry);
        }
    }
}