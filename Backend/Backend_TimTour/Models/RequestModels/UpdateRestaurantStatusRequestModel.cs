using Backend_TimTour.Models.LocationModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class UpdateRestaurantStatusRequestModel
    {
        [Required]
        public string restaurantName { get; set; }
        [Required]
        public string restaurantStatus { get; set; }
    }
}
