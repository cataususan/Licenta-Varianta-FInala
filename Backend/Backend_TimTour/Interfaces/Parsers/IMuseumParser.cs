using Backend_TimTour.Models.LocationModels;

namespace Backend_TimTour.Interfaces.Parsers
{
    public interface IMuseumParser
    {
        Task<(Museum, bool)> ParseRequestToMuseum(RequestMuseum museumToParse);
        RequestMuseum ParseMuseumToDatabaseObject(Museum museumToParse);
    }
}
