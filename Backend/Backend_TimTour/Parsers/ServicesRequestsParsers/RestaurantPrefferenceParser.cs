namespace Backend_TimTour.Parsers.ServicesRequestsParsers
{
    public class RestaurantPrefferenceParser
    {
        public static object CreatePreference(
            string priceRange,
            string specialFeatures,
            string dietaryRestrictions,
            string cuisineTypes,
            string atmosphere)
        {
            return new
            {
                preference = new
                {
                    priceRange,
                    specialFeatures,
                    dietaryRestrictions,
                    cuisineTypes,
                    atmosphere
                }
            };
        }
    }
}
