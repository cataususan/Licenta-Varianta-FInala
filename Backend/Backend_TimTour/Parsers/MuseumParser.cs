using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationEnums;
using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.LocationModels;

namespace Backend_TimTour.Parsers
{
    public class MuseumParser:IMuseumParser
    {
        public async Task<(Museum, bool)> ParseRequestToMuseum(RequestMuseum museumToParse)
        {
            MuseumAccesibility museumAccesibility;
            MuseumExhibitsTypes museumExhibitsTypes;
            MuseumTypes museumTypes;
            MuseumVisitorService museumVisitorService;
            UniversalPriceRange museumUniversalPriceRange;
            
            Museum parsedMuseum = new Museum();

            bool parseAccesibility = Enum.TryParse(museumToParse.Features.museumAccesibility, out museumAccesibility);
            bool parseExhibitsTypes = Enum.TryParse(museumToParse.Features.museumExhibitsTypes, out museumExhibitsTypes);
            bool parseTypes = Enum.TryParse(museumToParse.Features.museumTypes, out museumTypes);
            bool parseVisitorService = Enum.TryParse(museumToParse.Features.museumVisitorService, out museumVisitorService);
            bool parsePrice = Enum.TryParse(museumToParse.Features.PriceRange, out museumUniversalPriceRange);
            if (parseAccesibility && parseExhibitsTypes && parseVisitorService && parseTypes && parsePrice)
            {
                var newFeatures = new MuseumFeature
                {
                    museumAccesibility = museumAccesibility,
                    museumExhibitsTypes = museumExhibitsTypes,
                    museumVisitorService = museumVisitorService,
                    museumTypes = museumTypes,
                    PriceRange = museumUniversalPriceRange
                };
                parsedMuseum = new Museum
                {

                    Name = museumToParse.Name,
                    Location = museumToParse.Location,
                    Adress = museumToParse.Adress,
                    Rating = museumToParse.Rating,
                    Schedule = museumToParse.Schedule,
                    Status = museumToParse.Status,
                    Features = newFeatures,
                };
                return (parsedMuseum, true);
            }
            else
            {
                return (parsedMuseum, false);
            }

        }
        public RequestMuseum ParseMuseumToDatabaseObject(Museum museumToParse)
        {
            RequestMuseum parsedMuseum = new RequestMuseum();

            string parseAccesibility = museumToParse.Features.museumAccesibility.ToString();
            string parseExhibitsTypes = museumToParse.Features.museumExhibitsTypes.ToString();
            string parseTypes = museumToParse.Features.museumTypes.ToString();
            string parseVisitorService = museumToParse.Features.museumVisitorService.ToString();
            string parsePrice = museumToParse.Features.PriceRange.ToString();
            var newFeatures = new RequestMuseumFeature
            {
                museumAccesibility = parseAccesibility,
                museumExhibitsTypes = parseExhibitsTypes,
                museumVisitorService = parseVisitorService,
                museumTypes = parseTypes,
                PriceRange = parsePrice
            };
            parsedMuseum = new RequestMuseum
            {

                Name = museumToParse.Name,
                Location = museumToParse.Location,
                Adress = museumToParse.Adress,
                Rating = museumToParse.Rating,
                Schedule = museumToParse.Schedule,
                Status = museumToParse.Status,
                Features = newFeatures,
            };
            return (parsedMuseum);
        }
    }
}
