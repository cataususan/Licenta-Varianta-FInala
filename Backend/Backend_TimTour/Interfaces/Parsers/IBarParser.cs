using Backend_TimTour.Models.LocationModels;

namespace Backend_TimTour.Interfaces.Parsers
{
    public interface IBarParser
    {
        Task<(Bar, bool)> ParseRequestToBar(RequestBar barToParse);
        RequestBar ParseBarToDatabaseObject(Bar barToParse);
    }
}
