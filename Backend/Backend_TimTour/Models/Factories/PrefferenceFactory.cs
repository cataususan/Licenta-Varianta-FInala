using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.PrefferenceModels;

namespace Backend_TimTour.Models.Factories
{
    public class PrefferenceFactory
    {
        public static Prefference CreateEmptyPrefference()
        {
            var prefference = new Prefference { BarFeatures = new BarFeatures(),
                EventFeatures=new EventFeature(),
                MuseumFeatures=new MuseumFeature(),
                RestaurantFeatures =new RestaurantFeature()};

            return prefference;

        }
    }
}
