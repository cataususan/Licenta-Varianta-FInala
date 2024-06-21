using Backend_TimTour.Models.LocationModels;

namespace Backend_TimTour.Interfaces.Parsers
{
    public interface IEventParser
    {
        Task<(Event, bool)> ParseRequestToEvent(RequestEvent eventToParse);
        RequestEvent ParseEventToDatabaseObject(Event eventToParse);
    }
}
