using Backend_TimTour.Models.LocationEnums;

namespace Backend_TimTour.Models.LocationModels.LocationFeatures
{
    public class RequestRestaurantFeature
    {
        public string atmosphere { get; set; }
        public string cusineTypes { get; set; }
        public string dietaryRestrictions { get; set; }
        public string specialFeatures { get; set; }
        public string priceRange { get; set; }
    }
}
