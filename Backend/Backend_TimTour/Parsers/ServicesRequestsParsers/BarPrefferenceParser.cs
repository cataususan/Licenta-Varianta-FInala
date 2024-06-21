namespace Backend_TimTour.Parsers.ServicesRequestsParsers
{
    public class BarPrefferenceParser
    {
        public static object CreatePreference(
            string priceRange,
            string barAmbiance,
            string barDrinkSpecialties,
            string barEvent,
            string barFoodOptions)
        {
            return new
            {
                preference = new
                {
                    priceRange,
                    barAmbiance,
                    barDrinkSpecialties,
                    barEvent,
                    barFoodOptions
                }
            };
        }
    }
}
