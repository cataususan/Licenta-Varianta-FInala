using Backend_TimTour.Models.LocationModels.LocationFeatures;

namespace Backend_TimTour.Models.PrefferenceModels
{
    public class Prefference
    {
        public BarFeatures BarFeatures { get; set; }
        public EventFeature EventFeatures { get; set; }
        public MuseumFeature MuseumFeatures { get; set; }
        public RestaurantFeature RestaurantFeatures { get; set; }
    }
}
