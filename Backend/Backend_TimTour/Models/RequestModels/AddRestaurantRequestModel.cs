using Backend_TimTour.Models.LocationModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class AddRestaurantRequestModel
    {
        [Required]
        public RequestRestaurant restaurant { get; set; }
    }
}
