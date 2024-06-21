namespace Backend_TimTour.Parsers.ServicesRequestsParsers
{
    public class MuseumPrefferenceParser
    {
        public static object CreatePreference(
            string priceRange,
            string museumAccesibility,
            string museumExhibitsTypes,
            string museumTypes,
            string museumVisitorService)
        {
            return new
            {
                preference = new
                {
                    priceRange,
                    museumAccesibility,
                    museumExhibitsTypes,
                    museumTypes,
                    museumVisitorService
                }
            };
        }
    }
}
