using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationEnums;
using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.LocationModels;

namespace Backend_TimTour.Parsers
{
    public class EventParser:IEventParser
    {
        public async Task<(Event, bool)> ParseRequestToEvent(RequestEvent eventToParse)
        {
            EventAudience eventAudience;
            EventDuration eventDuration;
            EventGenre eventGenre;
            EventTypes eventTypes;
            EventVenue eventVenue;
            UniversalPriceRange eventPriceRange;
            Event parsedEvent = new Event();

            bool parseAudience = Enum.TryParse(eventToParse.Features.eventAudience, out eventAudience);
            bool parseDuration = Enum.TryParse(eventToParse.Features.eventDuration, out eventDuration);
            bool parseGenre = Enum.TryParse(eventToParse.Features.eventGenre, out eventGenre);
            bool parseTypes = Enum.TryParse(eventToParse.Features.eventTypes, out eventTypes);
            bool parseVenue = Enum.TryParse(eventToParse.Features.eventVenue, out eventVenue);
            bool parsePrice = Enum.TryParse(eventToParse.Features.eventPrice, out eventPriceRange);
            if (parseAudience && parseDuration && parseGenre && parseTypes && parseVenue && parsePrice)
            {
                var newFeatures = new EventFeature
                {
                    eventAudience = eventAudience,
                    eventDuration = eventDuration,
                    eventGenre = eventGenre,
                    eventTypes = eventTypes,
                    eventVenue = eventVenue,
                    eventPrice = eventPriceRange
                };
                parsedEvent = new Event
                {
                    
                    Name = eventToParse.Name,
                    Location = eventToParse.Location,
                    Adress = eventToParse.Adress,
                    EventDate = eventToParse.EventDate,
                    Status = eventToParse.Status,
                    Features = newFeatures,
                };
                return (parsedEvent, true);
            }
            else
            {
                return (parsedEvent, false);
            }

        }
        public RequestEvent ParseEventToDatabaseObject(Event eventToParse)
        {
            RequestEvent parsedEvent = new RequestEvent();

            string parseAudience = eventToParse.Features.eventAudience.ToString();
            string parseDuration = eventToParse.Features.eventDuration.ToString();
            string parseGenre = eventToParse.Features.eventGenre.ToString();
            string parseTypes = eventToParse.Features.eventTypes.ToString();
            string parseVenue = eventToParse.Features.eventVenue.ToString();
            string parsePrice = eventToParse.Features.eventPrice.ToString();
                var newFeatures = new RequestEventFeature
                {
                    eventAudience = parseAudience,
                    eventDuration = parseDuration,
                    eventGenre = parseGenre,
                    eventTypes = parseTypes,
                    eventVenue = parseVenue,
                    eventPrice = parsePrice
                };
                parsedEvent = new RequestEvent
                {

                    Name = eventToParse.Name,
                    Location = eventToParse.Location,
                    Adress = eventToParse.Adress,
                    EventDate = eventToParse.EventDate,
                    Status = eventToParse.Status,
                    Features = newFeatures,
                };
                return (parsedEvent);
        }
    }
}
