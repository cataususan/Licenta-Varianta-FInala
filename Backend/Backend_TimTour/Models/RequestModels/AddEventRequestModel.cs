using Backend_TimTour.Models.LocationModels;
using System.ComponentModel.DataAnnotations;

namespace Backend_TimTour.Models.RequestModels
{
    public class AddEventRequestModel
    {
        [Required]
        public RequestEvent eventAdded { get; set; }
    }
}
