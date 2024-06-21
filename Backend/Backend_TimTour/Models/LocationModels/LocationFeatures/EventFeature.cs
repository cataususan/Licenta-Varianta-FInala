using Backend_TimTour.Models.LocationEnums;

namespace Backend_TimTour.Models.LocationModels.LocationFeatures
{
    public class EventFeature
    {
        public EventAudience eventAudience { get; set; }
        public EventDuration eventDuration { get; set; }
        public EventGenre eventGenre { get; set; }
        public EventTypes eventTypes { get; set; }
        public EventVenue eventVenue { get; set; }
        public UniversalPriceRange eventPrice { get; set; }
    }
}
