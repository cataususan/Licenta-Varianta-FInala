using Backend_TimTour.Models.LocationEnums;

namespace Backend_TimTour.Models.LocationModels.LocationFeatures
{
    public class RestaurantFeature
    {
        public RestaurantAtmosphere atmosphere { get; set; }
        public RestaurantCusineTypes cusineTypes { get; set; }
        public RestaurantDietaryRestrictions dietaryRestrictions { get; set; }
        public RestaurantSpecialFeatures specialFeatures { get; set; }
        public UniversalPriceRange priceRange { get; set; }
    }
}
