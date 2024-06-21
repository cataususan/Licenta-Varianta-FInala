using Backend_TimTour.Models.LocationEnums;

namespace Backend_TimTour.Models.LocationModels.LocationFeatures
{
    public class BarFeatures
    {
        public BarType Type { get; set; }
        public BarEvents barEvent { get; set; }
        public BarAmbiance barAmbiance { get; set; }
        public BarDrinkSpecialties barDrinkSpecialties { get; set; }
        public BarFoodOptions barFoodOptions { get; set; }
        public UniversalPriceRange PriceRange { get; set; }
    }
}
