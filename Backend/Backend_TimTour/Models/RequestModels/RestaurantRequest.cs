using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class RestaurantRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
