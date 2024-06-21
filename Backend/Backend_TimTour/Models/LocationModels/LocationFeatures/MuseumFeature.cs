using Backend_TimTour.Models.LocationEnums;

namespace Backend_TimTour.Models.LocationModels.LocationFeatures
{
    public class MuseumFeature
    {
        public MuseumAccesibility museumAccesibility { get; set; }
        public MuseumExhibitsTypes museumExhibitsTypes { get; set; }
        public MuseumTypes museumTypes { get; set; }
        public MuseumVisitorService museumVisitorService { get; set; }
        public UniversalPriceRange PriceRange { get; set; }
    }
}
