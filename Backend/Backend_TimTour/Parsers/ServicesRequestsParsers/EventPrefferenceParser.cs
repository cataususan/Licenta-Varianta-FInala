namespace Backend_TimTour.Parsers.ServicesRequestsParsers
{
    public class EventPrefferenceParser
    {
        public static object CreatePreference(
            string eventPrice,
            string eventAudience,
            string eventDuration,
            string eventGenre,
            string eventTypes,
            string eventVenue)
        {
            return new
            {
                preference = new
                {
                    eventPrice,
                    eventAudience,
                    eventDuration,
                    eventGenre,
                    eventTypes,
                    eventVenue
                }
            };
        }
    }
}
