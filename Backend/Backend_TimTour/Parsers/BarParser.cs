using Amazon.Runtime.Internal;
using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationEnums;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.PrefferenceModels;

namespace Backend_TimTour.Parsers
{
    public class BarParser:IBarParser
    {
        public async Task<(Bar,bool)> ParseRequestToBar(RequestBar barToParse)
        {
            BarAmbiance barAmbiance;
            BarDrinkSpecialties barDrinkSpecialties;
            BarEvents barEvents;
            BarFoodOptions barFoodOptions;
            BarType barType;
            Bar parsedBar = new Bar();

            bool parseAmbiance = Enum.TryParse(barToParse.Features.barAmbiance, out barAmbiance);
            bool parseDrinks = Enum.TryParse(barToParse.Features.barDrinkSpecialties, out barDrinkSpecialties);
            bool parseEvents = Enum.TryParse(barToParse.Features.barEvent, out barEvents);
            bool parseFood = Enum.TryParse(barToParse.Features.barFoodOptions, out barFoodOptions);
            bool parseType = Enum.TryParse(barToParse.Features.Type, out barType);
            if(parseAmbiance && parseDrinks && parseEvents && parseFood && parseType) {
                var newFeatures = new BarFeatures
                {
                    barAmbiance = barAmbiance,
                    barDrinkSpecialties = barDrinkSpecialties,
                    barEvent = barEvents,
                    barFoodOptions = barFoodOptions,
                    Type = barType
                };
                parsedBar = new Bar
                {
                    Name = barToParse.Name,
                    Rating = barToParse.Rating,
                    Location = barToParse.Location,
                    Adress = barToParse.Adress,
                    Schedule = barToParse.Schedule,
                    Status = barToParse.Status,
                    Features = newFeatures,
                };
                return (parsedBar, true);
            }
            else
            {
                return (parsedBar, false);
            }
            
        }
        public RequestBar ParseBarToDatabaseObject(Bar barToParse)
        {
            string parsedAmbiance = barToParse.Features.barAmbiance.ToString();
            string parsedDrinks = barToParse.Features.barDrinkSpecialties.ToString();
            string parsedEvents = barToParse.Features.barEvent.ToString();
            string parsedFood = barToParse.Features.barFoodOptions.ToString();
            string parsedType = barToParse.Features.Type.ToString();
            string parsedPriceRange = barToParse.Features.PriceRange.ToString();

            var newFeatures = new RequestBarFeatures
            {

                barAmbiance = parsedAmbiance,
                barDrinkSpecialties = parsedDrinks,
                barEvent = parsedEvents,
                barFoodOptions = parsedFood,
                PriceRange = parsedPriceRange,
                Type = parsedType
            };
            var parsedBar = new RequestBar
            {
                Name = barToParse.Name,
                Rating = barToParse.Rating,
                Location = barToParse.Location,
                Adress = barToParse.Adress,
                Schedule = barToParse.Schedule,
                Status = barToParse.Status,
                Features = newFeatures,
            };
            return (parsedBar);
        }
    }
}
