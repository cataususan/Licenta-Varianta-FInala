using Backend_TimTour.Models.LocationEnums;
using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Interfaces.Parsers;

namespace Backend_TimTour.Parsers
{
    public class RestaurantParser:IRestaurantParser
    {
        public async Task<(Restaurant, bool)> ParseRequestToRestaurant(RequestRestaurant restaurantToParse)
        {
            RestaurantAtmosphere restaurantAtmosphere;
            RestaurantCusineTypes restaurantCusineTypes;
            RestaurantDietaryRestrictions restaurantDietaryRestrictions;
            RestaurantSpecialFeatures restaurantSpecialFeatures;
            UniversalPriceRange restaurantUniversalPriceRange;

            Restaurant parsedRestaurant = new Restaurant();

            bool parseAtmosphere = Enum.TryParse(restaurantToParse.Features.atmosphere, out restaurantAtmosphere);
            bool parseCusineTypes = Enum.TryParse(restaurantToParse.Features.cusineTypes, out restaurantCusineTypes);
            bool parseDietaryRestrictions = Enum.TryParse(restaurantToParse.Features.dietaryRestrictions, out restaurantDietaryRestrictions);
            bool parseSpecialFeatures = Enum.TryParse(restaurantToParse.Features.specialFeatures, out restaurantSpecialFeatures);
            bool parsePrice = Enum.TryParse(restaurantToParse.Features.priceRange, out restaurantUniversalPriceRange);
            if (parseAtmosphere && parseCusineTypes && parseDietaryRestrictions && parseSpecialFeatures && parsePrice)
            {
                var newFeatures = new RestaurantFeature
                {
                    atmosphere = restaurantAtmosphere,
                    cusineTypes = restaurantCusineTypes,
                    dietaryRestrictions = restaurantDietaryRestrictions,
                    specialFeatures = restaurantSpecialFeatures,
                    priceRange = restaurantUniversalPriceRange
                };
                parsedRestaurant = new Restaurant
                {

                    Name = restaurantToParse.Name,
                    Location = restaurantToParse.Location,
                    Adress = restaurantToParse.Adress,
                    Rating = restaurantToParse.Rating,
                    Schedule = restaurantToParse.Schedule,
                    Status = restaurantToParse.Status,
                    Features = newFeatures,
                };
                return (parsedRestaurant, true);
            }
            else
            {
                return (parsedRestaurant, false);
            }

        }
        public RequestRestaurant ParseRestaurantToDatabaseObject(Restaurant restaurantToParse)
        {
            RequestRestaurant parsedRestaurant = new RequestRestaurant();

            string parseAtmosphere = restaurantToParse.Features.atmosphere.ToString();
            string parseCusineTypes = restaurantToParse.Features.cusineTypes.ToString();
            string parseDietaryRestrictions = restaurantToParse.Features.dietaryRestrictions.ToString();
            string parseSpecialFeatures = restaurantToParse.Features.specialFeatures.ToString();
            string parsePrice = restaurantToParse.Features.priceRange.ToString();

            var newFeatures = new RequestRestaurantFeature
            {
                atmosphere = parseAtmosphere,
                cusineTypes = parseCusineTypes,
                dietaryRestrictions = parseDietaryRestrictions,
                specialFeatures = parseSpecialFeatures,
                priceRange = parsePrice,
            };
            parsedRestaurant = new RequestRestaurant
            {

                Name = restaurantToParse.Name,
                Location = restaurantToParse.Location,
                Adress = restaurantToParse.Adress,
                Rating = restaurantToParse.Rating,
                Schedule = restaurantToParse.Schedule,
                Status = restaurantToParse.Status,
                Features = newFeatures,
            };
            return (parsedRestaurant);
        }
    }
}
