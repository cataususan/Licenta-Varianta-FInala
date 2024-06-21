using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationEnums;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.PrefferenceModels;

namespace Backend_TimTour.Parsers
{
    public class UserPrefferenceParser: IUserPrefferenceParser
    {
        public async Task<(Prefference, bool)> ParseRequestToPrefference(RequestBarFeatures barFeatureToParse, RequestEventFeature eventFeatureToParse, RequestMuseumFeature museumFeatureToParse, RequestRestaurantFeature restaurantFeatureToParse)
        {
            Prefference prefference = new Prefference();
            BarAmbiance barAmbiance;
            BarDrinkSpecialties barDrinkSpecialties;
            BarEvents barEvents;
            BarFoodOptions barFoodOptions;
            BarType barType;
            EventAudience eventAudience;
            EventDuration eventDuration;
            EventGenre eventGenre;
            EventTypes eventTypes;
            EventVenue eventVenue;
            UniversalPriceRange eventPriceRange;
            MuseumAccesibility museumAccesibility;
            MuseumExhibitsTypes museumExhibitsTypes;
            MuseumTypes museumTypes;
            MuseumVisitorService museumVisitorService;
            UniversalPriceRange museumUniversalPriceRange;
            RestaurantAtmosphere restaurantAtmosphere;
            RestaurantCusineTypes restaurantCusineTypes;
            RestaurantDietaryRestrictions restaurantDietaryRestrictions;
            RestaurantSpecialFeatures restaurantSpecialFeatures;
            UniversalPriceRange restaurantUniversalPriceRange;

            bool parseAmbiance = Enum.TryParse(barFeatureToParse.barAmbiance, out barAmbiance);
            bool parseDrinks = Enum.TryParse(barFeatureToParse.barDrinkSpecialties, out barDrinkSpecialties);
            bool parseEvents = Enum.TryParse(barFeatureToParse.barEvent, out barEvents);
            bool parseFood = Enum.TryParse(barFeatureToParse.barFoodOptions, out barFoodOptions);
            bool parseType = Enum.TryParse(barFeatureToParse.Type, out barType);

            bool parseAtmosphere = Enum.TryParse(restaurantFeatureToParse.atmosphere, out restaurantAtmosphere);
            bool parseCusineTypes = Enum.TryParse(restaurantFeatureToParse.cusineTypes, out restaurantCusineTypes);
            bool parseDietaryRestrictions = Enum.TryParse(restaurantFeatureToParse.dietaryRestrictions, out restaurantDietaryRestrictions);
            bool parseSpecialFeatures = Enum.TryParse(restaurantFeatureToParse.specialFeatures, out restaurantSpecialFeatures);
            bool parsePrice = Enum.TryParse(restaurantFeatureToParse.priceRange, out restaurantUniversalPriceRange);

            bool parseAudience = Enum.TryParse(eventFeatureToParse.eventAudience, out eventAudience);
            bool parseDuration = Enum.TryParse(eventFeatureToParse.eventDuration, out eventDuration);
            bool parseGenre = Enum.TryParse(eventFeatureToParse.eventGenre, out eventGenre);
            bool parseTypes = Enum.TryParse(eventFeatureToParse.eventTypes, out eventTypes);
            bool parseVenue = Enum.TryParse(eventFeatureToParse.eventVenue, out eventVenue);
            bool parseEventPrice = Enum.TryParse(eventFeatureToParse.eventPrice, out eventPriceRange);

            bool parseAccesibility = Enum.TryParse(museumFeatureToParse.museumAccesibility, out museumAccesibility);
            bool parseExhibitsTypes = Enum.TryParse(museumFeatureToParse.museumExhibitsTypes, out museumExhibitsTypes);
            bool parseMuseumTypes = Enum.TryParse(museumFeatureToParse.museumTypes, out museumTypes);
            bool parseVisitorService = Enum.TryParse(museumFeatureToParse.museumVisitorService, out museumVisitorService);
            bool parseMuseumPrice = Enum.TryParse(museumFeatureToParse.PriceRange, out museumUniversalPriceRange);

            if (parseAmbiance && parseDrinks && parseEvents && parseFood && parseType)
            {
                var newBarFeatures = new BarFeatures
                {
                    barAmbiance = barAmbiance,
                    barDrinkSpecialties = barDrinkSpecialties,
                    barEvent = barEvents,
                    barFoodOptions = barFoodOptions,
                    Type = barType
                };
                prefference.BarFeatures = newBarFeatures;
            }
            else
            {
                return (prefference, false);
            }
            if (parseAtmosphere && parseCusineTypes && parseDietaryRestrictions && parseSpecialFeatures && parsePrice)
            {
                var newRestaurantFeatures = new RestaurantFeature
                {
                    atmosphere = restaurantAtmosphere,
                    cusineTypes = restaurantCusineTypes,
                    dietaryRestrictions = restaurantDietaryRestrictions,
                    specialFeatures = restaurantSpecialFeatures,
                    priceRange = restaurantUniversalPriceRange
                };
                prefference.RestaurantFeatures= newRestaurantFeatures;
            }
            else
            {
                return (prefference, false);
            }
            if (parseAudience && parseDuration && parseGenre && parseTypes && parseVenue && parseEventPrice)
            {
                var newEventFeatures = new EventFeature
                {
                    eventAudience = eventAudience,
                    eventDuration = eventDuration,
                    eventGenre = eventGenre,
                    eventTypes = eventTypes,
                    eventVenue = eventVenue,
                    eventPrice = eventPriceRange
                };
                prefference.EventFeatures= newEventFeatures;
            }
            else
            {
                return (prefference, false);
            }
            if (parseAccesibility && parseExhibitsTypes && parseVisitorService && parseMuseumTypes && parseMuseumTypes)
            {
                var newMuseumFeatures = new MuseumFeature
                {
                    museumAccesibility = museumAccesibility,
                    museumExhibitsTypes = museumExhibitsTypes,
                    museumVisitorService = museumVisitorService,
                    museumTypes = museumTypes,
                    PriceRange = museumUniversalPriceRange
                };
                prefference.MuseumFeatures= newMuseumFeatures;
            }
            else
            {
                return (prefference, false);
            }

            return (prefference, true);

            
        }
    }
}
