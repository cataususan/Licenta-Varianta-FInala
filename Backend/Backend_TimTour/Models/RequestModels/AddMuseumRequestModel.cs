using Backend_TimTour.Models.LocationModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class AddMuseumRequestModel
    {
        [Required]
        public RequestMuseum museum { get; set; }
    }
}
