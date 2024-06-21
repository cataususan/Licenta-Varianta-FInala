using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.PrefferenceModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class PostPrefferenceRequestModel
    {
        [Required]
        public RequestBarFeatures BarFeatures { get; set; }
        [Required]
        public RequestEventFeature EventFeatures { get; set; }
        [Required]
        public RequestMuseumFeature MuseumFeatures { get; set; }
        [Required]
        public RequestRestaurantFeature RestaurantFeatures { get; set; }
    }
}
